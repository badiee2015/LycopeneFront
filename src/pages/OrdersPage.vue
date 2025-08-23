<template>
  <q-page class="q-pa-md bg-grey-1">
    <div class="text-h5 q-mb-lg text-weight-medium">سفارش‌های من</div>
    
    <div v-if="orders.length === 0" class="text-center q-py-xl">
      <q-icon name="receipt_long" size="80px" color="grey-5" />
      <div class="text-h6 text-grey-6 q-mt-md">سفارشی یافت نشد</div>
      <div class="text-body2 text-grey-5 q-mt-sm">هنوز سفارشی ثبت نکرده‌اید</div>
      <q-btn
        color="primary"
        label="مشاهده محصولات"
        class="q-mt-md"
        @click="$router.push('/')"
      />
    </div>

    <div v-else class="q-gutter-md">
      <q-card
        v-for="order in orders"
        :key="order.id"
        flat
        bordered
        class="bg-white"
      >
        <q-card-section>
          <div class="row items-center justify-between">
            <div>
              <div class="text-body1 text-weight-medium">سفارش #{{ order.id }}</div>
              <div class="text-caption text-grey-6">{{ order.date }}</div>
            </div>
            <q-chip
              :color="getStatusColor(order.status)"
              text-color="white"
              :label="getStatusLabel(order.status)"
            />
          </div>
          
          <q-separator class="q-my-md" />
          
          <div class="row items-center justify-between">
            <div class="text-body2">مبلغ کل: {{ formatPrice(order.total) }}</div>
            <q-btn
              outline
              color="primary"
              size="sm"
              label="جزئیات"
              @click="viewOrder(order.id)"
            />
          </div>
        </q-card-section>
      </q-card>
    </div>
  </q-page>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter();

const orders = ref([
  {
    id: 12345,
    date: '۱۴۰۲/۱۲/۱۵',
    status: 'delivered',
    total: 2500000
  },
  {
    id: 12346,
    date: '۱۴۰۲/۱۲/۲۰',
    status: 'processing',
    total: 1200000
  }
]);

const getStatusColor = (status: string): string => {
  switch (status) {
    case 'delivered': return 'positive';
    case 'processing': return 'orange';
    case 'cancelled': return 'negative';
    default: return 'grey';
  }
};

const getStatusLabel = (status: string): string => {
  switch (status) {
    case 'delivered': return 'تحویل شده';
    case 'processing': return 'در حال پردازش';
    case 'cancelled': return 'لغو شده';
    default: return 'نامشخص';
  }
};

const formatPrice = (price: number): string => {
  return new Intl.NumberFormat('fa-IR').format(price) + ' تومان';
};

const viewOrder = (orderId: number) => {
  void router.push(`/orders/${orderId}`);
};
</script>
