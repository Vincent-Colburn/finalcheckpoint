import { createRouter, createWebHashHistory } from 'vue-router'
import { authGuard } from '@bcwdev/auth0provider-client'
// import { vaultService } from './services/VaultService'
// import { accountService } from './services/AccountService'

function loadPage(page) {
  return () => import(`./pages/${page}.vue`)
}

const routes = [
  {
    path: '/',
    name: 'Home',
    component: loadPage('HomePage')
  },
  {
    path: '/about',
    name: 'About',
    component: loadPage('AboutPage')
  },
  {
    path: '/profiles/:id',
    name: 'ProfileDetailsPage',
    component: loadPage('ProfileDetailsPage')
  },
  {
    path: '/vault/:id',
    name: 'VaultDetailsPage',
    component: loadPage('VaultDetailsPage')
    // beforeEnter: (to, from, next) => {
    // vault = vaultService.getVaultById(id),
    // account = accountService.getAccount(),
    // if(vault.IsPrivate === true) {
    //   if (vault.creatorId === account.id) {
    //     next()
    //   } else {
    //     const err = new Error('You cannot enter private vaults')
    //     next(err)
    //   }
    // } else {
    //   next()
    // }
    // }
  },
  {
    path: '/account',
    name: 'Account',
    component: loadPage('AccountPage'),
    beforeEnter: authGuard
  }
]

const router = createRouter({
  linkActiveClass: 'router-link-active',
  linkExactActiveClass: 'router-link-exact-active',
  history: createWebHashHistory(),
  routes
})

export default router
