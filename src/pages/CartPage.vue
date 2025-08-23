<template>
  <q-page class="q-pa-md bg-grey-1">
    <div class="text-h5 q-mb-lg text-weight-medium">سبد خرید</div>
    
    <div v-if="cartItems.length === 0" class="text-center q-py-xl">
      <q-icon name="shopping_cart" size="80px" color="grey-5" />
      <div class="text-h6 text-grey-6 q-mt-md">سبد خرید شما خالی است</div>
      <div class="text-body2 text-grey-5 q-mt-sm">محصولات مورد نظر خود را به سبد اضافه کنید</div>
      <q-btn
        color="primary"
        label="مشاهده محصولات"
        class="q-mt-md"
        @click="$router.push('/')"
      />
    </div>

    <div v-else>
      <!-- Cart Items -->
      <div class="q-gutter-md q-mb-lg">
        <q-card
          v-for="item in cartItems"
          :key="item.id"
          flat
          bordered
          class="bg-white"
        >
          <q-card-section class="row q-gutter-md">
            <q-img
              :src="item.image"
              :alt="item.name"
              width="80px"
              height="80px"
              class="rounded-borders"
            />
            
            <div class="col">
              <div class="text-body1 q-mb-xs">{{ item.name }}</div>
              <div class="text-body2 text-primary text-weight-medium q-mb-sm">
                {{ formatPrice(item.price) }}
              </div>
              
              <div class="row items-center justify-between">
                <div class="row items-center q-gutter-sm">
                  <q-btn
                    round
                    dense
                    size="sm"
                    icon="remove"
                    @click="decreaseQuantity(item.id)"
                    :disable="item.quantity <= 1"
                  />
                  <span class="text-body1">{{ item.quantity }}</span>
                  <q-btn
                    round
                    dense
                    size="sm"
                    icon="add"
                    @click="increaseQuantity(item.id)"
                  />
                </div>
                
                <q-btn
                  flat
                  dense
                  icon="delete"
                  color="negative"
                  @click="removeItem(item.id)"
                />
              </div>
            </div>
          </q-card-section>
        </q-card>
      </div>

      <!-- Cart Summary -->
      <q-card flat bordered class="bg-white">
        <q-card-section>
          <div class="text-h6 q-mb-md">خلاصه سبد خرید</div>
          
          <div class="row justify-between q-mb-sm">
            <span>قیمت کالاها ({{ totalItems }} کالا)</span>
            <span>{{ formatPrice(subtotal) }}</span>
          </div>
          
          <div class="row justify-between q-mb-sm">
            <span>هزینه ارسال</span>
            <span class="text-positive">رایگان</span>
          </div>
          
          <q-separator class="q-my-md" />
          
          <div class="row justify-between text-h6 text-weight-medium">
            <span>مبلغ قابل پرداخت</span>
            <span class="text-primary">{{ formatPrice(subtotal) }}</span>
          </div>
        </q-card-section>
        
        <q-card-actions>
          <q-btn
            color="primary"
            label="ادامه فرآیند خرید"
            class="full-width"
            size="lg"
            @click="proceedToCheckout"
          />
        </q-card-actions>
      </q-card>
    </div>
  </q-page>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue';
import { useRouter } from 'vue-router';
import { useQuasar } from 'quasar';

const router = useRouter();
const $q = useQuasar();

const cartItems = ref([
  {
    id: 1,
    name: 'گوشی موبایل سامسونگ Galaxy A54',
    price: 12500000,
    quantity: 1,
    image: 'https://via.placeholder.com/80x80/e3f2fd/1976d2?text=Phone'
  },
  {
    id: 2,
    name: 'هدفون بی‌سیم ایرپاد',
    price: 3500000,
    quantity: 2,
    image: 'https://via.placeholder.com/80x80/e8f5e8/388e3c?text=Headphone'
  }
]);

const totalItems = computed(() => {
  return cartItems.value.reduce((sum, item) => sum + item.quantity, 0);
});

const subtotal = computed(() => {
  return cartItems.value.reduce((sum, item) => sum + (item.price * item.quantity), 0);
});

const formatPrice = (price: number): string => {
  return new Intl.NumberFormat('fa-IR').format(price) + ' تومان';
};

const increaseQuantity = (itemId: number) => {
  const item = cartItems.value.find(item => item.id === itemId);
  if (item) {
    item.quantity++;
  }
};

const decreaseQuantity = (itemId: number) => {
  const item = cartItems.value.find(item => item.id === itemId);
  if (item && item.quantity > 1) {
    item.quantity--;
  }
};

const removeItem = (itemId: number) => {
  $q.dialog({
    title: 'حذف کالا',
    message: 'آیا مطمئن هستید که می‌خواهید این کالا را از سبد خرید حذف کنید؟',
    cancel: {
      label: 'انصراف',
      color: 'grey',
      flat: true
    },
    ok: {
      label: 'حذف',
      color: 'negative'
    },
    persistent: true
  }).onOk(() => {
    const index = cartItems.value.findIndex(item => item.id === itemId);
    if (index > -1) {
      cartItems.value.splice(index, 1);
    }
    $q.notify({
      type: 'positive',
      message: 'کالا از سبد خرید حذف شد',
      position: 'top'
    });
  });
};

const proceedToCheckout = () => {
  void router.push('/checkout');
};
</script>