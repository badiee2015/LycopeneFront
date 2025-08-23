import type { RouteRecordRaw } from 'vue-router';

const routes: RouteRecordRaw[] = [
  // Digikala-like layout routes
  {
    path: '/',
    component: () => import('layouts/DigikalaLayout.vue'),
    children: [
      { 
        path: '', 
        name: 'Home',
        component: () => import('pages/DigiHomePage.vue') 
      },
      { 
        path: '/orders', 
        name: 'Orders',
        component: () => import('pages/OrdersPage.vue') 
      },
      { 
        path: '/cart', 
        name: 'Cart',
        component: () => import('pages/CartPage.vue') 
      },
      { 
        path: '/profile', 
        name: 'Profile',
        component: () => import('pages/ProfilePage.vue') 
      }
    ],
  },

  // Test layout routes (existing)
  {
    path: '/test',
    component: () => import('layouts/MainLayout.vue'),
    children: [
      { 
        path: '', 
        name: 'TestList',
        component: () => import('pages/TestListPage.vue') 
      },
      { 
        path: ':testId', 
        name: 'TestWizard',
        component: () => import('pages/TestWizardPage.vue') 
      },
      { 
        path: 'result/:testId', 
        name: 'TestResult',
        component: () => import('pages/TestResultPage.vue') 
      }
    ],
  },

  // Always leave this as last one
  {
    path: '/:catchAll(.*)*',
    component: () => import('pages/ErrorNotFound.vue'),
  },
];

export default routes;