<template>
  <q-layout view="lHh Lpr lFf">
    <!-- Header -->
    <q-header elevated class="bg-white text-dark">
      <q-toolbar>
        <!-- Hamburger Menu -->
        <q-btn
          flat
          dense
          round
          icon="menu"
          aria-label="Menu"
          @click="toggleLeftDrawer"
          color="grey-8"
        />

        <!-- Center Title -->
        <q-toolbar-title class="text-center text-primary">
          لیکوپن
        </q-toolbar-title>

        <!-- Empty space for balance -->
        <q-btn flat dense round style="visibility: hidden" />
      </q-toolbar>
    </q-header>

    <!-- Left Drawer -->
    <q-drawer
      v-model="leftDrawerOpen"
      show-if-above
      bordered
      :width="280"
      :breakpoint="400"
      class="bg-grey-1"
    >
      <q-scroll-area class="fit">
        <!-- User Profile Section -->
        <div class="q-pa-md bg-primary text-white">
          <div class="row items-center q-gutter-md">
            <q-avatar size="50px" color="white" text-color="primary" icon="person" />
            <div>
              <div class="text-body1 text-weight-medium">خوش آمدید</div>
              <div class="text-caption">وارد حساب کاربری خود شوید</div>
            </div>
          </div>
        </div>

        <!-- Menu Items -->
        <q-list>
          <q-item-label header class="text-grey-8">
            دسته‌بندی‌ها
          </q-item-label>

          <q-item clickable v-ripple>
            <q-item-section avatar>
              <q-icon name="smartphone" color="primary" />
            </q-item-section>
            <q-item-section>
              <q-item-label>کالای دیجیتال</q-item-label>
            </q-item-section>
          </q-item>

          <q-item clickable v-ripple>
            <q-item-section avatar>
              <q-icon name="checkroom" color="primary" />
            </q-item-section>
            <q-item-section>
              <q-item-label>مد و پوشاک</q-item-label>
            </q-item-section>
          </q-item>

          <q-item clickable v-ripple>
            <q-item-section avatar>
              <q-icon name="home" color="primary" />
            </q-item-section>
            <q-item-section>
              <q-item-label>خانه و آشپزخانه</q-item-label>
            </q-item-section>
          </q-item>

          <q-item clickable v-ripple>
            <q-item-section avatar>
              <q-icon name="sports_soccer" color="primary" />
            </q-item-section>
            <q-item-section>
              <q-item-label>ورزش و سفر</q-item-label>
            </q-item-section>
          </q-item>

          <q-item clickable v-ripple>
            <q-item-section avatar>
              <q-icon name="health_and_safety" color="primary" />
            </q-item-section>
            <q-item-section>
              <q-item-label>زیبایی و سلامت</q-item-label>
            </q-item-section>
          </q-item>

          <q-item clickable v-ripple>
            <q-item-section avatar>
              <q-icon name="toys" color="primary" />
            </q-item-section>
            <q-item-section>
              <q-item-label>کودک و نوزاد</q-item-label>
            </q-item-section>
          </q-item>

          <q-separator class="q-my-md" />

          <q-item-label header class="text-grey-8">
            خدمات
          </q-item-label>

          <q-item clickable v-ripple>
            <q-item-section avatar>
              <q-icon name="support_agent" color="orange" />
            </q-item-section>
            <q-item-section>
              <q-item-label>پشتیبانی</q-item-label>
            </q-item-section>
          </q-item>

          <q-item clickable v-ripple>
            <q-item-section avatar>
              <q-icon name="info" color="blue" />
            </q-item-section>
            <q-item-section>
              <q-item-label>درباره ما</q-item-label>
            </q-item-section>
          </q-item>
        </q-list>
      </q-scroll-area>
    </q-drawer>

    <!-- Main Page Container -->
    <q-page-container>
      <router-view />
    </q-page-container>

    <!-- Bottom Navigation Footer -->
    <q-footer class="bg-white text-dark shadow-up-2">
      <q-tabs
        v-model="activeTab"
        class="text-grey-6"
        active-color="primary"
        indicator-color="transparent"
        align="justify"
        narrow-indicator
      >
        <q-tab
          name="home"
          icon="home"
          label="خانه"
          @click="navigateTo('/')"
          :class="{ 'text-primary': activeTab === 'home' }"
        />
        
        <q-tab
          name="orders"
          icon="receipt_long"
          label="سفارش‌ها"
          @click="navigateTo('/orders')"
          :class="{ 'text-primary': activeTab === 'orders' }"
        />
        
        <q-tab
          name="cart"
          icon="shopping_cart"
          label="سبد خرید"
          @click="navigateTo('/cart')"
          :class="{ 'text-primary': activeTab === 'cart' }"
        >
          <!-- Cart Badge -->
          <q-badge
            v-if="cartItemsCount > 0"
            color="negative"
            floating
            :label="cartItemsCount"
            style="top: 8px; right: 20px;"
          />
        </q-tab>
        
        <q-tab
          name="profile"
          icon="person"
          label="پروفایل"
          @click="navigateTo('/profile')"
          :class="{ 'text-primary': activeTab === 'profile' }"
        />
      </q-tabs>
    </q-footer>
  </q-layout>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue';
import { useRouter, useRoute } from 'vue-router';

const router = useRouter();
const route = useRoute();

const leftDrawerOpen = ref(false);
const activeTab = ref('home');
const cartItemsCount = ref(3); // This would come from your cart store

const toggleLeftDrawer = () => {
  leftDrawerOpen.value = !leftDrawerOpen.value;
};

const navigateTo = (path: string) => {
  void router.push(path);
};

// Update active tab based on current route
const updateActiveTab = () => {
  const path = route.path;
  if (path === '/') {
    activeTab.value = 'home';
  } else if (path.startsWith('/orders')) {
    activeTab.value = 'orders';
  } else if (path.startsWith('/cart')) {
    activeTab.value = 'cart';
  } else if (path.startsWith('/profile')) {
    activeTab.value = 'profile';
  }
};

// Watch route changes to update active tab
watch(() => route.path, updateActiveTab, { immediate: true });
</script>

<style scoped>
.q-footer {
  border-top: 1px solid #e0e0e0;
}

.q-tab {
  min-height: 60px;
  font-size: 11px;
}

.q-tab__content {
  gap: 4px;
}

.q-tab__icon {
  font-size: 22px;
}

.q-toolbar-title {
  font-family: 'Vazirmatn', sans-serif;
  font-weight: bold;
  font-size: 18px;
}

/* Custom styles for better mobile experience */
@media (max-width: 599.98px) {
  .q-tab {
    min-width: auto;
    padding: 8px 4px;
  }
  
  .q-tab__label {
    font-size: 10px;
  }
  
  .q-tab__icon {
    font-size: 20px;
  }
}
</style>
