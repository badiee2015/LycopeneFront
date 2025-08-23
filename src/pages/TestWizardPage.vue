<template>
  <q-page class="q-pa-md">
    <div v-if="!testStarted && currentTest" class="text-center">
      <div class="text-h4 q-mb-md">{{ currentTest.title }}</div>
      <div class="text-body1 q-mb-lg">{{ currentTest.description }}</div>
      
      <q-card class="q-ma-auto" style="max-width: 400px">
        <q-card-section>
          <div class="text-h6 q-mb-md">جزئیات آزمون</div>
          <div class="q-mb-sm">
            <q-icon name="quiz" color="primary" class="q-mr-sm" />
            تعداد سوالات: {{ currentTest.totalQuestions }}
          </div>
          <div class="q-mb-sm">
            <q-icon name="timer" color="orange" class="q-mr-sm" />
            مدت زمان: {{ currentTest.duration }} دقیقه
          </div>
        </q-card-section>
        
        <q-card-actions>
          <q-btn 
            color="primary" 
            label="شروع آزمون"
            @click="startTestWizard"
            class="full-width"
          />
        </q-card-actions>
      </q-card>
    </div>

    <div v-else-if="testStarted && !testCompleted" class="test-wizard">
      <!-- Progress bar -->
      <q-linear-progress 
        :value="progress" 
        color="primary"
        class="q-mb-lg"
        style="height: 8px"
      />
      
      <!-- Timer -->
      <div class="text-center q-mb-lg">
        <q-chip 
          :color="timeLeft < 300 ? 'negative' : 'primary'" 
          text-color="white"
          icon="timer"
        >
          {{ formatTime(timeLeft) }}
        </q-chip>
      </div>
      
      <!-- Question counter -->
      <div class="text-center q-mb-md">
        <span class="text-h6">
          سوال {{ currentQuestionIndex + 1 }} از {{ currentTest?.totalQuestions }}
        </span>
      </div>
      
      <!-- Question -->
      <q-card class="q-mb-lg" v-if="currentQuestion">
        <q-card-section>
          <div class="text-h6 q-mb-lg">{{ currentQuestion.text }}</div>
          
          <q-option-group
            v-model="selectedAnswer"
            :options="questionOptions"
            color="primary"
            type="radio"
            class="q-gutter-sm"
          />
        </q-card-section>
      </q-card>
      
      <!-- Navigation buttons -->
      <div class="row q-gutter-sm justify-between">
        <q-btn
          v-if="currentQuestionIndex > 0"
          outline
          color="primary"
          label="قبلی"
          icon="arrow_forward"
          @click="previousQuestion"
          class="col-auto"
        />
        
        <q-space />
        
        <q-btn
          v-if="currentQuestionIndex < totalQuestions - 1"
          color="primary"
          label="بعدی"
          icon-right="arrow_back"
          @click="nextQuestion"
          :disable="selectedAnswer === null"
          class="col-auto"
        />
        
        <q-btn
          v-else
          color="positive"
          label="اتمام آزمون"
          icon-right="check"
          @click="finishTest"
          :disable="selectedAnswer === null"
          class="col-auto"
        />
      </div>
    </div>

    <q-inner-loading :showing="loading">
      <q-spinner-gears size="50px" color="primary" />
    </q-inner-loading>
  </q-page>
</template>

<script setup lang="ts">
import { ref, computed, onMounted, onUnmounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import type { Test } from '../types/test';

const route = useRoute();
const router = useRouter();

const loading = ref(false);
const currentTest = ref<Test | null>(null);
const testStarted = ref(false);
const testCompleted = ref(false);
const currentQuestionIndex = ref(0);
const selectedAnswer = ref<number | null | undefined>(null);
const answers = ref<(number | null | undefined)[]>([]);
const startTime = ref<Date | null>(null);
const timeLeft = ref(0);
const timer = ref<NodeJS.Timeout | null>(null);

const mockTests: Test[] = [
  // Same mock data as TestListPage
  {
    id: 1,
    title: 'آزمون ریاضی پایه',
    description: 'آزمون مقدماتی ریاضی شامل جمع، تفریق، ضرب و تقسیم',
    duration: 30,
    totalQuestions: 10,
    questions: [
      {
        id: 1,
        text: '5 + 3 = ?',
        options: ['6', '7', '8', '9'],
        correctAnswer: 2
      },
      {
        id: 2,
        text: '12 - 7 = ?',
        options: ['4', '5', '6', '7'],
        correctAnswer: 1
      },
      {
        id: 3,
        text: '4 × 6 = ?',
        options: ['20', '22', '24', '26'],
        correctAnswer: 2
      },
      {
        id: 4,
        text: '15 ÷ 3 = ?',
        options: ['3', '4', '5', '6'],
        correctAnswer: 2
      },
      {
        id: 5,
        text: '8 + 7 = ?',
        options: ['14', '15', '16', '17'],
        correctAnswer: 1
      },
      {
        id: 6,
        text: '20 - 12 = ?',
        options: ['6', '7', '8', '9'],
        correctAnswer: 2
      },
      {
        id: 7,
        text: '3 × 9 = ?',
        options: ['25', '26', '27', '28'],
        correctAnswer: 2
      },
      {
        id: 8,
        text: '18 ÷ 2 = ?',
        options: ['8', '9', '10', '11'],
        correctAnswer: 1
      },
      {
        id: 9,
        text: '6 + 8 = ?',
        options: ['12', '13', '14', '15'],
        correctAnswer: 2
      },
      {
        id: 10,
        text: '25 - 16 = ?',
        options: ['7', '8', '9', '10'],
        correctAnswer: 2
      }
    ]
  }
];

const progress = computed(() => {
  if (!currentTest.value) return 0;
  return (currentQuestionIndex.value + 1) / currentTest.value.totalQuestions;
});

const totalQuestions = computed(() => currentTest.value?.totalQuestions || 0);

const currentQuestion = computed(() => {
  if (!currentTest.value || currentQuestionIndex.value >= currentTest.value.questions.length) {
    return null;
  }
  return currentTest.value.questions[currentQuestionIndex.value];
});

const questionOptions = computed(() => {
  if (!currentQuestion.value) return [];
  return currentQuestion.value.options.map((option, index) => ({
    label: option,
    value: index
  }));
});

const formatTime = (seconds: number) => {
  const minutes = Math.floor(seconds / 60);
  const secs = seconds % 60;
  return `${minutes.toString().padStart(2, '0')}:${secs.toString().padStart(2, '0')}`;
};

const loadTest = async () => {
  loading.value = true;
  try {
    const testId = parseInt(route.params.testId as string);
    // Simulate API call
    await new Promise(resolve => setTimeout(resolve, 500));
    
    const test = mockTests.find(t => t.id === testId);
    if (!test) {
      throw new Error('Test not found');
    }
    
    currentTest.value = test;
    answers.value = new Array(test.totalQuestions).fill(null);
  } catch (error) {
    console.error('Error loading test:', error);
    void router.push('/');
  } finally {
    loading.value = false;
  }
};

const startTestWizard = () => {
  if (!currentTest.value) return;
  
  testStarted.value = true;
  startTime.value = new Date();
  timeLeft.value = currentTest.value.duration * 60;
  selectedAnswer.value = answers.value[0];
  
  // Start timer
  timer.value = setInterval(() => {
    timeLeft.value--;
    if (timeLeft.value <= 0) {
      void finishTest();
    }
  }, 1000);
};

const nextQuestion = () => {
  if (selectedAnswer.value !== null) {
    answers.value[currentQuestionIndex.value] = selectedAnswer.value;
  }
  
  if (currentQuestionIndex.value < totalQuestions.value - 1) {
    currentQuestionIndex.value++;
    selectedAnswer.value = answers.value[currentQuestionIndex.value];
  }
};

const previousQuestion = () => {
  if (selectedAnswer.value !== null) {
    answers.value[currentQuestionIndex.value] = selectedAnswer.value;
  }
  
  if (currentQuestionIndex.value > 0) {
    currentQuestionIndex.value--;
    selectedAnswer.value = answers.value[currentQuestionIndex.value];
  }
};

const finishTest = async () => {
  if (selectedAnswer.value !== null) {
    answers.value[currentQuestionIndex.value] = selectedAnswer.value;
  }
  
  if (timer.value) {
    clearInterval(timer.value);
  }
  
  const endTime = new Date();
  const timeTaken = startTime.value 
    ? Math.floor((endTime.getTime() - startTime.value.getTime()) / 1000)
    : 0;
  
  // Submit test results
  try {
    loading.value = true;
    
    // Calculate score (mock)
    let correctAnswers = 0;
    const details = answers.value.map((answer, index) => {
      const question = currentTest.value?.questions[index];
      const isCorrect = answer === question?.correctAnswer;
      if (isCorrect) correctAnswers++;
      
      return {
        questionId: question?.id || index + 1,
        selectedAnswer: answer || -1,
        isCorrect
      };
    });
    
    const testResult = {
      testId: currentTest.value?.id || 0,
      score: Math.round((correctAnswers / totalQuestions.value) * 100),
      totalQuestions: totalQuestions.value,
      correctAnswers,
      timeTaken,
      passed: correctAnswers >= Math.ceil(totalQuestions.value * 0.6),
      details
    };
    
    // Simulate API call to api/report
    await new Promise(resolve => setTimeout(resolve, 1000));
    
    void router.push({
      name: 'TestResult',
      params: { 
        testId: currentTest.value?.id.toString() || '0'
      },
      query: {
        result: encodeURIComponent(JSON.stringify(testResult))
      }
    });
    
  } catch (error) {
    console.error('Error submitting test:', error);
  } finally {
    loading.value = false;
  }
};

onMounted(() => {
  void loadTest();
});

onUnmounted(() => {
  if (timer.value) {
    clearInterval(timer.value);
  }
});
</script>

<style scoped>
.test-wizard {
  max-width: 800px;
  margin: 0 auto;
}

@media (max-width: 600px) {
  .test-wizard {
    padding: 0 8px;
  }
}
</style>
