import { createRouter, createWebHistory } from 'vue-router'
import LoginView from '../views/Login.vue'
import RegisterView from '../views/Register.vue'
import HomeView from '../views/Home.vue'
import AccountView from '../views/Account.vue'
import ConfirmationView from '../views/confirmation.vue'
import ContactView from '../views/Contact.vue'
import AppAccountview from '../views/App-account.vue'
import CreateView from '../views/Create.vue'
import GoogleAuth from '../views/GoogleAuth.vue'
import AdminView from '../views/Admin.vue'
import MyAreaView from '../views/MyArea.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
    },
    {
      path: '/login',
      name: 'login',
      component: LoginView
    },
    {
        path: '/register',
        name: 'register',
        component: RegisterView
    },
    {
      path: '/Account',
      name: 'account',
      component: AccountView
    },
    {
      path: '/confirmation',
      name: 'confirmation',
      component: ConfirmationView
    },
    {
    path: '/Contact',
    name: 'contact',
    component: ContactView
    },
    {
    path: '/App-account',
    name: 'app-account',
    component: AppAccountview
    },
    {
    path: '/Create',
    name: 'create',
    component: CreateView
    },
    {
      path: '/google',
      name: 'google',
      component: GoogleAuth
    },
    {
      path: '/Admin',
      name: 'Admin',
      component: AdminView
    },
    {
      path: '/MyArea',
      name: 'MyArea',
      component: MyAreaView
    }
  ]
})

export default router
