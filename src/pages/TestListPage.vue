<template>
  <q-page class="q-pa-md">
    <div class="text-h4 q-mb-lg text-center">
      آزمون‌های موجود
    </div>
    
    <div class="row q-gutter-md">
      <div 
        v-for="test in tests" 
        :key="test.id"
        class="col-12 col-sm-6 col-md-4"
      >
        <q-card 
          class="test-card cursor-pointer"
          @click="startTest(test)"
          flat
          bordered
        >
          <q-card-section>
            <div class="text-h6 q-mb-sm">{{ test.title }}</div>
            <div class="text-body2 text-grey-7 q-mb-md">
              {{ test.description }}
            </div>
            
            <div class="row items-center q-gutter-sm text-caption">
              <q-icon name="quiz" color="primary" />
              <span>{{ test.totalQuestions }} سوال</span>
              
              <q-icon name="timer" color="orange" />
              <span>{{ test.duration }} دقیقه</span>
            </div>
          </q-card-section>
          
          <q-card-actions align="center">
            <q-btn 
              color="primary" 
              label="شروع آزمون"
              icon-right="arrow_back"
              @click.stop="startTest(test)"
              class="full-width"
            />
          </q-card-actions>
        </q-card>
      </div>
    </div>
    
    <q-inner-loading :showing="loading">
      <q-spinner-gears size="50px" color="primary" />
    </q-inner-loading>
  </q-page>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import type { Test } from '../types/test';

const router = useRouter();
const loading = ref(false);
const tests = ref<Test[]>([]);

// Mock data - replace with actual API call
const mockTests: Test[] = [
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
  },
  {
    id: 2,
    title: 'آزمون زبان فارسی',
    description: 'آزمون دستور زبان فارسی و املا',
    duration: 45,
    totalQuestions: 15,
    questions: [
      {
        id: 1,
        text: 'کدام گزینه درست نوشته شده است؟',
        options: ['می‌رود', 'میرود', 'می رود', 'مي رود'],
        correctAnswer: 0
      },
      {
        id: 2,
        text: 'جمع "کتاب" کدام است؟',
        options: ['کتاب‌ها', 'کتابها', 'کتاب ها', 'کتب'],
        correctAnswer: 3
      }
      // Add more questions as needed...
    ]
  },
  {
    id: 3,
    title: 'آزمون علوم',
    description: 'آزمون مفاهیم پایه علوم طبیعی',
    duration: 60,
    totalQuestions: 20,
    questions: [
      {
        id: 1,
        text: 'آب در چه دمایی یخ می‌زند؟',
        options: ['0 درجه سانتیگراد', '32 درجه سانتیگراد', '100 درجه سانتیگراد', '-10 درجه سانتیگراد'],
        correctAnswer: 0
      }
      // Add more questions as needed...
    ]
  }
];

const loadTests = async () => {
  loading.value = true;
  try {
    // Simulate API call
    await new Promise(resolve => setTimeout(resolve, 1000));
    tests.value = mockTests;
  } catch (error) {
    console.error('Error loading tests:', error);
  } finally {
    loading.value = false;
  }
};

const startTest = (test: Test) => {
  router.push({
    name: 'TestWizard',
    params: { testId: test.id.toString() }
  });
};

onMounted(():void => {
  loadTests();
});
</script>

<style scoped>
.test-card {
  transition: transform 0.2s, box-shadow 0.2s;
  height: 100%;
}

.test-card:hover {
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(0,0,0,0.15);
}

@media (max-width: 600px) {
  .test-card {
    margin-bottom: 16px;
  }
}
</style>
