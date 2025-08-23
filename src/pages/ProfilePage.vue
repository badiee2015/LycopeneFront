<template>
  <q-page class="bg-grey-1">
    <!-- Header Banner Section -->
    <div class="profile-header relative-position">
      <q-img
        src="https://images.unsplash.com/photo-1544005313-94ddf0286df2?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1000&q=80"
        height="200px"
        class="profile-bg-image"
      >
        <div class="absolute-full bg-gradient"></div>
      </q-img>
      
      <!-- Profile Info Overlay -->
      <div class="absolute-bottom q-pa-md text-white">
        <div class="row items-center q-gutter-md">
          <q-avatar size="80px" class="shadow-5">
            <q-img
              src="https://images.unsplash.com/photo-1494790108755-2616b612b786?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=150&q=80"
              alt="Profile"
            />
          </q-avatar>
          <div>
            <div class="text-h6 text-weight-medium">{{ user.name }}</div>
            <div class="text-body2 opacity-80">{{ user.phone }}</div>
            <div class="text-caption opacity-60">عضو از {{ user.memberSince }}</div>
          </div>
        </div>
      </div>

      <!-- Notification Badge -->
      <div class="absolute-top-right q-pa-md">
        <q-btn
          round
          color="orange"
          icon="notifications"
          size="sm"
          class="shadow-3"
        >
          <q-badge color="red" floating>{{ notifications.count }}</q-badge>
        </q-btn>
      </div>
    </div>

    <!-- DigiClub Banner -->
    <div class="q-pa-md">
      <q-card class="digiclub-card bg-gradient-purple text-white" flat>
        <q-card-section class="row items-center">
          <div class="col-8">
            <div class="row items-center q-gutter-xs q-mb-sm">
              <q-icon name="diamond" size="24px" />
              <span class="text-body1 text-weight-medium">با اشتراک پلاس</span>
            </div>
            <div class="text-caption opacity-80">
              سفارش خود را در کمترین زمان، رایگان تحویل بگیرید.
            </div>
          </div>
          <div class="col-4 text-left">
            <q-btn
              label="خرید اشتراک"
              color="white"
              text-color="purple"
              size="sm"
              dense
              rounded
            />
          </div>
        </q-card-section>
      </q-card>
    </div>

    <!-- Menu Items -->
    <div class="q-pa-md q-gutter-sm">
      <!-- Personal Information Section -->
      <q-card flat class="menu-card">
        <q-list>
          <q-item
            clickable
            v-ripple
            @click="navigateTo('/profile/addresses')"
            class="menu-item"
          >
            <q-item-section avatar>
              <q-icon name="location_on" size="24px" color="grey-7" />
            </q-item-section>
            <q-item-section>
              <q-item-label class="text-body1">آدرس‌ها</q-item-label>
            </q-item-section>
            <q-item-section side>
              <q-icon name="chevron_left" color="grey-5" />
            </q-item-section>
          </q-item>

          <q-separator />

          <q-item
            clickable
            v-ripple
            @click="navigateTo('/profile/reviews')"
            class="menu-item"
          >
            <q-item-section avatar>
              <q-icon name="rate_review" size="24px" color="grey-7" />
            </q-item-section>
            <q-item-section>
              <q-item-label class="text-body1">امتیاز و دیدگاه</q-item-label>
            </q-item-section>
            <q-item-section side>
              <q-icon name="chevron_left" color="grey-5" />
            </q-item-section>
          </q-item>

          <q-separator />

          <q-item
            clickable
            v-ripple
            @click="navigateTo('/profile/gift-cards')"
            class="menu-item"
          >
            <q-item-section avatar>
              <q-icon name="card_giftcard" size="24px" color="grey-7" />
            </q-item-section>
            <q-item-section>
              <q-item-label class="text-body1">کارت‌های هدیه</q-item-label>
            </q-item-section>
            <q-item-section side>
              <q-icon name="chevron_left" color="grey-5" />
            </q-item-section>
          </q-item>

          <q-separator />

          <q-item
            clickable
            v-ripple
            @click="navigateTo('/profile/discount-codes')"
            class="menu-item"
          >
            <q-item-section avatar>
              <q-icon name="local_offer" size="24px" color="grey-7" />
            </q-item-section>
            <q-item-section>
              <q-item-label class="text-body1">کدهای تخفیف</q-item-label>
            </q-item-section>
            <q-item-section side>
              <q-icon name="chevron_left" color="grey-5" />
            </q-item-section>
          </q-item>

          <q-separator />

          <q-item
            clickable
            v-ripple
            @click="navigateTo('/profile/recent-visits')"
            class="menu-item"
          >
            <q-item-section avatar>
              <q-icon name="history" size="24px" color="grey-7" />
            </q-item-section>
            <q-item-section>
              <q-item-label class="text-body1">بررسی‌های اخیر</q-item-label>
            </q-item-section>
            <q-item-section side>
              <q-icon name="chevron_left" color="grey-5" />
            </q-item-section>
          </q-item>
        </q-list>
      </q-card>

      <!-- Account Settings Section -->
      <q-card flat class="menu-card">
        <q-list>
          <q-item
            clickable
            v-ripple
            @click="navigateTo('/profile/edit')"
            class="menu-item"
          >
            <q-item-section avatar>
              <q-icon name="person" size="24px" color="grey-7" />
            </q-item-section>
            <q-item-section>
              <q-item-label class="text-body1">اطلاعات حساب کاربری</q-item-label>
            </q-item-section>
            <q-item-section side>
              <q-icon name="chevron_left" color="grey-5" />
            </q-item-section>
          </q-item>

          <q-separator />

          <q-item
            clickable
            v-ripple
            @click="navigateTo('/profile/wallet')"
            class="menu-item"
          >
            <q-item-section avatar>
              <q-icon name="account_balance_wallet" size="24px" color="grey-7" />
            </q-item-section>
            <q-item-section>
              <q-item-label class="text-body1">کیف پول</q-item-label>
            </q-item-section>
            <q-item-section side>
              <div class="row items-center q-gutter-xs">
                <span class="text-caption text-positive">{{ formatPrice(user.walletBalance) }}</span>
                <q-icon name="chevron_left" color="grey-5" />
              </div>
            </q-item-section>
          </q-item>

          <q-separator />

          <q-item
            clickable
            v-ripple
            @click="navigateTo('/profile/favorites')"
            class="menu-item"
          >
            <q-item-section avatar>
              <q-icon name="favorite" size="24px" color="grey-7" />
            </q-item-section>
            <q-item-section>
              <q-item-label class="text-body1">علاقه‌مندی‌ها</q-item-label>
            </q-item-section>
            <q-item-section side>
              <div class="row items-center q-gutter-xs">
                <q-badge color="primary" :label="user.favoritesCount" />
                <q-icon name="chevron_left" color="grey-5" />
              </div>
            </q-item-section>
          </q-item>
        </q-list>
      </q-card>

      <!-- Support Section -->
      <q-card flat class="menu-card">
        <q-list>
          <q-item
            clickable
            v-ripple
            @click="navigateTo('/support')"
            class="menu-item"
          >
            <q-item-section avatar>
              <q-icon name="help_outline" size="24px" color="grey-7" />
            </q-item-section>
            <q-item-section>
              <q-item-label class="text-body1">پشتیبانی</q-item-label>
            </q-item-section>
            <q-item-section side>
              <q-icon name="chevron_left" color="grey-5" />
            </q-item-section>
          </q-item>

          <q-separator />

          <q-item
            clickable
            v-ripple
            @click="navigateTo('/about')"
            class="menu-item"
          >
            <q-item-section avatar>
              <q-icon name="info_outline" size="24px" color="grey-7" />
            </q-item-section>
            <q-item-section>
              <q-item-label class="text-body1">درباره لیکوپن</q-item-label>
            </q-item-section>
            <q-item-section side>
              <q-icon name="chevron_left" color="grey-5" />
            </q-item-section>
          </q-item>

          <q-separator />

          <q-item
            clickable
            v-ripple
            @click="navigateTo('/settings')"
            class="menu-item"
          >
            <q-item-section avatar>
              <q-icon name="settings" size="24px" color="grey-7" />
            </q-item-section>
            <q-item-section>
              <q-item-label class="text-body1">تنظیمات</q-item-label>
            </q-item-section>
            <q-item-section side>
              <q-icon name="chevron_left" color="grey-5" />
            </q-item-section>
          </q-item>
        </q-list>
      </q-card>
    </div>

    <!-- Support Button -->
    <div class="q-pa-md">
      <q-btn
        color="orange"
        icon="headset_mic"
        label="نیاز به پشتیبانی دارم"
        class="full-width support-btn"
        rounded
        @click="openSupport"
      />
    </div>

    <!-- Logout -->
    <div class="q-pa-md q-pb-xl">
      <q-btn
        flat
        color="negative"
        icon="logout"
        label="خروج از حساب کاربری"
        class="full-width"
        @click="logout"
      />
    </div>
  </q-page>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { useQuasar } from 'quasar';

const router = useRouter();
const $q = useQuasar();

const user = ref({
  name: 'فاطمه احمدی',
  phone: '09123456789',
  memberSince: 'فروردین ۱۴۰۱',
  walletBalance: 250000,
  favoritesCount: 12
});

const notifications = ref({
  count: 3
});

const formatPrice = (price: number): string => {
  return new Intl.NumberFormat('fa-IR').format(price) + ' تومان';
};

const navigateTo = (path: string) => {
  void router.push(path);
};

const openSupport = () => {
  $q.dialog({
    title: 'پشتیبانی',
    message: 'آیا می‌خواهید با پشتیبانی تماس بگیرید؟',
    cancel: {
      label: 'انصراف',
      color: 'grey',
      flat: true
    },
    ok: {
      label: 'تماس',
      color: 'primary'
    },
    persistent: true
  }).onOk(() => {
    // Handle support contact
    $q.notify({
      type: 'positive',
      message: 'در حال اتصال به پشتیبانی...',
      position: 'top'
    });
  });
};

const logout = () => {
  $q.dialog({
    title: 'خروج از حساب کاربری',
    message: 'آیا مطمئن هستید که می‌خواهید از حساب کاربری خود خارج شوید؟',
    cancel: {
      label: 'انصراف',
      color: 'grey',
      flat: true
    },
    ok: {
      label: 'خروج',
      color: 'negative'
    },
    persistent: true
  }).onOk(() => {
    // Handle logout
    $q.notify({
      type: 'positive',
      message: 'با موفقیت خارج شدید',
      position: 'top'
    });
    void router.push('/login');
  });
};
</script>

<style scoped>
.profile-header {
  margin-bottom: -30px;
}

.profile-bg-image {
  border-radius: 0 0 20px 20px;
}

.bg-gradient {
  background: linear-gradient(to bottom, rgba(0,0,0,0.3), rgba(0,0,0,0.7));
}

.bg-gradient-purple {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
}

.digiclub-card {
  border-radius: 12px;
  overflow: hidden;
}

.menu-card {
  border-radius: 12px;
  overflow: hidden;
  margin-bottom: 8px;
}

.menu-item {
  padding: 16px;
  min-height: 64px;
}

.menu-item:hover {
  background-color: rgba(0,0,0,0.04);
}

.support-btn {
  height: 48px;
  font-weight: 500;
  font-size: 14px;
}

/* Mobile specific adjustments */
@media (max-width: 599.98px) {
  .profile-header {
    margin-bottom: -20px;
  }
  
  .menu-item {
    padding: 12px 16px;
    min-height: 56px;
  }
  
  .q-item__section--avatar {
    min-width: 40px;
  }
}

/* RTL specific adjustments */
.q-item__section--side {
  padding-right: 0;
  padding-left: 16px;
}

.q-item__section--avatar {
  padding-left: 0;
  padding-right: 16px;
}

/* Custom animations */
.menu-item {
  transition: background-color 0.2s ease;
}

/* Badge positioning for RTL */
.q-badge--floating {
  top: -6px;
  left: -6px;
  right: auto;
}
</style>