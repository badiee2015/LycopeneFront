import { ref } from 'vue';
import type { Test, TestResult } from '../types/test';

export function useTestApi() {
  const loading = ref(false);
  const error = ref<string | null>(null);

  // Mock API base URL
  const API_BASE = process.env.NODE_ENV === 'production' 
    ? '/api' 
    : 'http://localhost:3000/api';

  const fetchTests = async (): Promise<Test[]> => {
    loading.value = true;
    error.value = null;
    
    try {
      // In real implementation, replace with actual API call:
      // const response = await fetch(`${API_BASE}/tests`);
      // const data = await response.json();
      // return data;
      
      // Mock data for development
      await new Promise(resolve => setTimeout(resolve, 1000));
      
      return [
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
          totalQuestions: 8,
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
            },
            {
              id: 3,
              text: 'کدام جمله صحیح است؟',
              options: ['من کتاب می‌خوانم', 'من کتاب میخوانم', 'من کتاب می خوانم', 'من کتاب مي خوانم'],
              correctAnswer: 0
            },
            {
              id: 4,
              text: '"خورشید" در جمله نقش چه دارد؟ "خورشید می‌درخشد"',
              options: ['فاعل', 'مفعول', 'مسند', 'قید'],
              correctAnswer: 0
            },
            {
              id: 5,
              text: 'کدام کلمه اسم مشتق است؟',
              options: ['کتاب', 'نویسنده', 'قلم', 'میز'],
              correctAnswer: 1
            },
            {
              id: 6,
              text: 'جمع "آسمان" کدام است؟',
              options: ['آسمان‌ها', 'آسمانها', 'آسمان ها', 'آسامین'],
              correctAnswer: 0
            },
            {
              id: 7,
              text: 'کدام گزینه صفت تفضیلی است؟',
              options: ['بزرگ', 'بزرگتر', 'بزرگی', 'بزرگوار'],
              correctAnswer: 1
            },
            {
              id: 8,
              text: 'فعل "رفتن" در زمان گذشته چگونه است؟',
              options: ['رفت', 'می‌رود', 'خواهد رفت', 'برود'],
              correctAnswer: 0
            }
          ]
        },
        {
          id: 3,
          title: 'آزمون علوم پایه',
          description: 'آزمون مفاهیم پایه علوم طبیعی',
          duration: 60,
          totalQuestions: 12,
          questions: [
            {
              id: 1,
              text: 'آب در چه دمایی یخ می‌زند؟',
              options: ['0 درجه سانتیگراد', '32 درجه سانتیگراد', '100 درجه سانتیگراد', '-10 درجه سانتیگراد'],
              correctAnswer: 0
            },
            {
              id: 2,
              text: 'کدام گاز بیشترین درصد هوا را تشکیل می‌دهد؟',
              options: ['اکسیژن', 'نیتروژن', 'کربن دی اکسید', 'آرگون'],
              correctAnswer: 1
            },
            {
              id: 3,
              text: 'فتوسنتز در کدام قسمت گیاه انجام می‌شود؟',
              options: ['ریشه', 'ساقه', 'برگ', 'گل'],
              correctAnswer: 2
            },
            {
              id: 4,
              text: 'کدام سیاره به خورشید نزدیک‌تر است؟',
              options: ['زمین', 'مریخ', 'زهره', 'عطارد'],
              correctAnswer: 3
            },
            {
              id: 5,
              text: 'صدا در کدام محیط سریع‌تر منتشر می‌شود؟',
              options: ['هوا', 'آب', 'فلز', 'خلاء'],
              correctAnswer: 2
            },
            {
              id: 6,
              text: 'کدام عنصر در الماس وجود دارد؟',
              options: ['کربن', 'اکسیژن', 'هیدروژن', 'نیتروژن'],
              correctAnswer: 0
            },
            {
              id: 7,
              text: 'قلب انسان چند دهلیز دارد؟',
              options: ['یک', 'دو', 'سه', 'چهار'],
              correctAnswer: 1
            },
            {
              id: 8,
              text: 'کدام جانور خونگرم است؟',
              options: ['مار', 'ماهی', 'پرنده', 'قورباغه'],
              correctAnswer: 2
            },
            {
              id: 9,
              text: 'نور چه نوع انرژی است؟',
              options: ['مکانیکی', 'شیمیایی', 'الکتریکی', 'الکترومغناطیسی'],
              correctAnswer: 3
            },
            {
              id: 10,
              text: 'کدام گاز در اثر تنفس تولید می‌شود؟',
              options: ['اکسیژن', 'نیتروژن', 'کربن دی اکسید', 'هیدروژن'],
              correctAnswer: 2
            },
            {
              id: 11,
              text: 'آهنربا کدام فلزات را جذب می‌کند؟',
              options: ['آهن', 'مس', 'طلا', 'نقره'],
              correctAnswer: 0
            },
            {
              id: 12,
              text: 'کدام حشره کامل تبدیل شکل دارد؟',
              options: ['ملخ', 'پروانه', 'زنبور عسل', 'مورچه'],
              correctAnswer: 1
            }
          ]
        }
      ];
      
    } catch (err) {
      error.value = err instanceof Error ? err.message : 'خطا در بارگذاری آزمون‌ها';
      throw err;
    } finally {
      loading.value = false;
    }
  };

  const fetchTest = async (testId: number): Promise<Test | null> => {
    loading.value = true;
    error.value = null;
    
    try {
      // In real implementation:
      // const response = await fetch(`${API_BASE}/tests/${testId}`);
      // const data = await response.json();
      // return data;
      
      const tests = await fetchTests();
      return tests.find(test => test.id === testId) || null;
      
    } catch (err) {
      error.value = err instanceof Error ? err.message : 'خطا در بارگذاری آزمون';
      throw err;
    } finally {
      loading.value = false;
    }
  };

  const submitTestResult = async (testId: number, answers: (number | null)[], timeTaken: number): Promise<TestResult> => {
    loading.value = true;
    error.value = null;
    
    try {
      // In real implementation:
      // const response = await fetch(`${API_BASE}/report`, {
      //   method: 'POST',
      //   headers: { 'Content-Type': 'application/json' },
      //   body: JSON.stringify({ testId, answers, timeTaken })
      // });
      // const result = await response.json();
      // return result;
      
      // Mock implementation
      await new Promise(resolve => setTimeout(resolve, 1500));
      
      const test = await fetchTest(testId);
      if (!test) {
        throw new Error('آزمون یافت نشد');
      }
      
      let correctAnswers = 0;
      const details = answers.map((answer, index) => {
        const question = test.questions[index];
        const isCorrect = answer === question?.correctAnswer;
        if (isCorrect) correctAnswers++;
        
        return {
          questionId: question?.id || index + 1,
          selectedAnswer: answer || -1,
          isCorrect
        };
      });
      
      const score = Math.round((correctAnswers / test.totalQuestions) * 100);
      const passed = correctAnswers >= Math.ceil(test.totalQuestions * 0.6); // 60% pass rate
      
      return {
        testId,
        score,
        totalQuestions: test.totalQuestions,
        correctAnswers,
        timeTaken,
        passed,
        details
      };
      
    } catch (err) {
      error.value = err instanceof Error ? err.message : 'خطا در ارسال نتایج';
      throw err;
    } finally {
      loading.value = false;
    }
  };

  return {
    loading,
    error,
    fetchTests,
    fetchTest,
    submitTestResult
  };
}
