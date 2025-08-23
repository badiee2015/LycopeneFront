<template>
  <q-page class="bg-grey-1">
    <!-- Search Bar -->
    <div class="q-pa-md bg-white shadow-1">
      <q-input
        v-model="searchText"
        placeholder="جستجو در لیکوپن..."
        outlined
        dense
        class="bg-grey-1"
      >
        <template v-slot:prepend>
          <q-icon name="search" />
        </template>
      </q-input>
    </div>

    <!-- Banner Section -->
    <div class="q-pa-md">
      <q-carousel
        v-model="slide"
        swipeable
        animated
        navigation
        infinite
        :autoplay="autoplay"
        arrows
        height="160px"
        class="rounded-borders shadow-2"
      >
        <q-carousel-slide
          v-for="(banner, index) in banners"
          :key="index"
          :name="index"
          class="column no-wrap flex-center"
          :style="`background: linear-gradient(45deg, ${banner.color1}, ${banner.color2})`"
        >
          <div class="text-white text-center">
            <div class="text-h6 q-mb-sm">{{ banner.title }}</div>
            <div class="text-body2">{{ banner.subtitle }}</div>
          </div>
        </q-carousel-slide>
      </q-carousel>
    </div>

    <!-- Categories Grid -->
    <div class="q-pa-md">
      <div class="text-h6 q-mb-md text-weight-medium">دسته‌بندی‌ها</div>
      <div class="row q-gutter-sm">
        <div
          v-for="category in categories"
          :key="category.id"
          class="col-6 col-sm-4 col-md-3"
        >
          <q-card
            flat
            bordered
            class="category-card cursor-pointer bg-white"
            @click="navigateToCategory(category.id)"
          >
            <q-card-section class="text-center q-pa-md">
              <q-avatar size="50px" :color="category.color" class="q-mb-sm">
                <q-icon :name="category.icon" size="24px" color="white" />
              </q-avatar>
              <div class="text-body2 text-weight-medium">{{ category.name }}</div>
            </q-card-section>
          </q-card>
        </div>
      </div>
    </div>

    <!-- Featured Products -->
    <div class="q-pa-md">
      <div class="text-h6 q-mb-md text-weight-medium">محصولات ویژه</div>
      <q-scroll-area
        horizontal
        style="height: 280px"
        class="bg-white rounded-borders"
      >
        <div class="row no-wrap q-gutter-sm q-pa-sm" style="width: max-content">
          <div
            v-for="product in featuredProducts"
            :key="product.id"
            style="width: 160px"
          >
            <q-card class="product-card cursor-pointer" @click="navigateToProduct(product.id)">
              <q-img
                :src="product.image"
                :alt="product.name"
                height="120px"
                class="rounded-borders-top"
              />
              <q-card-section class="q-pa-sm">
                <div class="text-caption text-grey-8 ellipsis-2-lines">
                  {{ product.name }}
                </div>
                <div class="row items-center justify-between q-mt-xs">
                  <div class="text-weight-bold text-primary">
                    {{ formatPrice(product.price) }}
                  </div>
                  <q-chip
                    v-if="product.discount"
                    size="sm"
                    color="negative"
                    text-color="white"
                    :label="`${product.discount}%`"
                  />
                </div>
              </q-card-section>
            </q-card>
          </div>
        </div>
      </q-scroll-area>
    </div>
  </q-page>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter();

const searchText = ref('');
const slide = ref(0);
const autoplay = ref(3000);

const banners = [
  {
    title: 'تخفیف ویژه',
    subtitle: 'تا ۵۰٪ تخفیف روی محصولات منتخب',
    color1: '#ff6b6b',
    color2: '#ee5a24'
  },
  {
    title: 'ارسال رایگان',
    subtitle: 'برای خریدهای بالای ۵۰۰ هزار تومان',
    color1: '#74b9ff',
    color2: '#0984e3'
  },
  {
    title: 'محصولات جدید',
    subtitle: 'جدیدترین محصولات را کشف کنید',
    color1: '#55a3ff',
    color2: '#3742fa'
  }
];

const categories = [
  { id: 1, name: 'کالای دیجیتال', icon: 'smartphone', color: 'blue' },
  { id: 2, name: 'مد و پوشاک', icon: 'checkroom', color: 'pink' },
  { id: 3, name: 'خانه و آشپزخانه', icon: 'home', color: 'green' },
  { id: 4, name: 'ورزش و سفر', icon: 'sports_soccer', color: 'orange' },
  { id: 5, name: 'زیبایی و سلامت', icon: 'health_and_safety', color: 'purple' },
  { id: 6, name: 'کودک و نوزاد', icon: 'toys', color: 'cyan' }
];

const featuredProducts = [
  {
    id: 1,
    name: 'گوشی موبایل سامسونگ Galaxy A54',
    price: 12500000,
    discount: 15,
    image: 'https://via.placeholder.com/160x120/e3f2fd/1976d2?text=Phone'
  },
  {
    id: 2,
    name: 'لپ تاپ ایسوس VivoBook',
    price: 25000000,
    discount: 10,
    image: 'https://via.placeholder.com/160x120/f3e5f5/7b1fa2?text=Laptop'
  },
  {
    id: 3,
    name: 'هدفون بی‌سیم ایرپاد',
    price: 3500000,
    discount: 20,
    image: 'https://via.placeholder.com/160x120/e8f5e8/388e3c?text=Headphone'
  },
  {
    id: 4,
    name: 'ساعت هوشمند اپل واچ',
    price: 8000000,
    discount: null,
    image: 'https://via.placeholder.com/160x120/fff3e0/f57c00?text=Watch'
  }
];

const formatPrice = (price: number): string => {
  return new Intl.NumberFormat('fa-IR').format(price) + ' تومان';
};

const navigateToCategory = (categoryId: number) => {
  void router.push(`/category/${categoryId}`);
};

const navigateToProduct = (productId: number) => {
  void router.push(`/product/${productId}`);
};
</script>

<style scoped>
.category-card {
  transition: transform 0.2s, box-shadow 0.2s;
}

.category-card:hover {
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(0,0,0,0.15);
}

.product-card {
  transition: transform 0.2s, box-shadow 0.2s;
  border-radius: 8px;
  overflow: hidden;
}

.product-card:hover {
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(0,0,0,0.15);
}

.ellipsis-2-lines {
  display: -webkit-box;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
  overflow: hidden;
  text-overflow: ellipsis;
  line-height: 1.2;
  max-height: 2.4em;
}
</style>
