export interface Question {
  id: number;
  text: string;
  options: string[];
  correctAnswer?: number; // For mock purposes
}

export interface Test {
  id: number;
  title: string;
  description: string;
  duration: number; // in minutes
  questions: Question[];
  totalQuestions: number;
}

export interface TestResult {
  testId: number;
  score: number;
  totalQuestions: number;
  correctAnswers: number;
  timeTaken: number; // in seconds
  passed: boolean;
  details: {
    questionId: number;
    selectedAnswer: number;
    isCorrect: boolean;
  }[];
}