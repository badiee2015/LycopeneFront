<template>
  <q-page class="q-pa-md">
    <div class="result-container">
      <div class="text-center q-mb-xl">
        <q-avatar 
          :icon="testResult?.passed ? 'check_circle' : 'cancel'"
          :color="testResult?.passed ? 'positive' : 'negative'"
          size="80px"
          class="q-mb-md"
        />
        
        <div class="text-h4 q-mb-sm">
          {{ testResult?.passed ? 'تبریک!' : 'متأسفانه' }}
        </div>
        
        <div class="text-h6 text-grey-7">
          {{ testResult?.passed ? 'در آزمون قبول شدید' : 'در آزمون قبول نشدید' }}
        </div>
      </div>

      <q-card v-if="testResult" class="q-mb-lg">
        <q-card-section>
          <div class="text-h6 q-mb-md">نتایج آزمون</div>
          
          <div class="row q-gutter-lg">
            <div class="col-12 col-sm-6">
              <q-circular-progress
                :value="testResult.score"
                size="120px"
                :thickness="0.15"
                :color="testResult.passed ? 'positive' : 'negative'"
                track-color="grey-3"
                class="q-ma-md"
                show-value
              >
                <div class="text-h6">{{ testResult.score }}%</div>
              </q-circular-progress>
            </div>
            
            <div class="col-12 col-sm-5">
              <div class="q-mb-sm">
                <q-icon name="quiz" color="primary" class="q-mr-sm" />
                پاسخ صحیح: {{ testResult.correctAnswers }} از {{ testResult.totalQuestions }}
              </div>
              
              <div class="q-mb-sm">
                <q-icon name="timer" color="orange" class="q-mr-sm" />
                زمان صرف شده: {{ formatTime(testResult.timeTaken) }}
              </div>
              
              <div class="q-mb-sm">
                <q-icon 
                  :name="testResult.passed ? 'check_circle' : 'cancel'" 
                  :color="testResult.passed ? 'positive' : 'negative'" 
                  class="q-mr-sm" 
                />
                وضعیت: {{ testResult.passed ? 'قبول' : 'مردود' }}
              </div>
            </div>
          </div>
        </q-card-section>
      </q-card>

      <!-- Detailed results -->
      <q-expansion-item 
        v-if="testResult"
        icon="visibility" 
        label="مشاهده جزئیات"
        class="q-mb-lg"
      >
        <q-card>
          <q-card-section>
            <div class="text-h6 q-mb-md">جزئیات پاسخ‌ها</div>
            
            <div v-for="(detail, index) in testResult.details" :key="detail.questionId" class="q-mb-md">
              <div class="row items-center q-gutter-sm q-mb-xs">
                <q-badge 
                  :color="detail.isCorrect ? 'positive' : 'negative'"
                  :label="index + 1"
                />
                <q-icon 
                  :name="detail.isCorrect ? 'check' : 'close'"
                  :color="detail.isCorrect ? 'positive' : 'negative'"
                />
                <span class="text-body2">
                  {{ detail.isCorrect ? 'صحیح' : 'غلط' }}
                </span>
              </div>
              
              <q-separator v-if="index < testResult.details.length - 1" class="q-my-sm" />
            </div>
          </q-card-section>
        </q-card>
      </q-expansion-item>

      <!-- Action buttons -->
      <div class="row q-gutter-sm justify-center">
        <q-btn
          outline
          color="primary"
          label="آزمون‌های دیگر"
          icon="quiz"
          @click="goToTests"
          class="col-auto"
        />
        
        <q-btn
          v-if="testResult && !testResult.passed"
          color="primary"
          label="تکرار آزمون"
          icon="refresh"
          @click="retakeTest"
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
import { ref, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import type { TestResult } from '../types/test';

const route = useRoute();
const router = useRouter();

const loading = ref(false);
const testResult = ref<TestResult | null>(null);

const formatTime = (seconds: number) => {
  const minutes = Math.floor(seconds / 60);
  const secs = seconds % 60;
  return `${minutes} دقیقه و ${secs} ثانیه`;
};

const loadResult = async () => {
  loading.value = true;
  try {
    // Get result from query params (in real app, fetch from API)
    const resultData = route.query.result as string;
    if (resultData) {
      testResult.value = JSON.parse(decodeURIComponent(resultData));
    } else {
      // Fallback - fetch from API using testId
      const testId = route.params.testId;
      // Simulate API call to api/report
      await new Promise(resolve => setTimeout(resolve, 1000));
      
      // Mock result for demonstration
      testResult.value = {
        testId: parseInt(testId as string),
        score: 85,
        totalQuestions: 10,
        correctAnswers: 8,
        timeTaken: 1200,
        passed: true,
        details: []
      };
    }
  } catch (error) {
    console.error('Error loading result:', error);
    void router.push('/');
  } finally {
    loading.value = false;
  }
};

const goToTests = () => {
  void router.push('/');
};

const retakeTest = () => {
  const testId = route.params.testId;
  void router.push({
    name: 'TestWizard',
    params: { testId }
  });
};

onMounted(() => {
  void loadResult();
});
</script>

<style scoped>
.result-container {
  max-width: 600px;
  margin: 0 auto;
}

@media (max-width: 600px) {
  .result-container {
    padding: 0 8px;
  }
}
</style>
