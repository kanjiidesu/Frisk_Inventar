import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import AccountViewRegister from '../views/AccountViewRegister.vue'
import AccountLoginView from '../views/AccountViewLogin.vue'
import AccountView from '../views/AccountView.vue'
import NotificationManager from '../views/NotificationManager.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/fridge',
      name: 'fridge',
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('../views/FridgeView.vue')
    },
    {
      path: '/account-register',
      name: 'account-register',
      component: AccountViewRegister
    },
    {
      path: '/account-login',
      name: 'account-login',
      component: AccountLoginView
    },
    {
      path: '/account',
      name: 'account',
      component: AccountView
    },
    {
      path: '/notification-manager',
      name: 'notification-manager',
      component: NotificationManager
    }
  ]
})

export default router
