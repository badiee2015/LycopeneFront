import type { RouteRecordRaw } from 'vue-router';

const routes: RouteRecordRaw[] = [
  {
    path: '/',
    component: () => import('layouts/MainLayout.vue'),
    children: [
      { 
        path: '', 
        name: 'TestList',
        component: () => import('pages/TestListPage.vue') 
      },
      { 
        path: '/test/:testId', 
        name: 'TestWizard',
        component: () => import('pages/TestWizardPage.vue') 
      },
      { 
        path: '/result/:testId', 
        name: 'TestResult',
        component: () => import('pages/TestResultPage.vue') 
      }
    ],
  },

  // Always leave this as last one,
  // but you can also remove it
  {
    path: '/:catchAll(.*)*',
    component: () => import('pages/ErrorNotFound.vue'),
  },
];

export default routes;
