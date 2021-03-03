<template>
  <div class="container-fluid">
    <div class="row py-5 mx-4">
      <div class="col-12">
        <h1> {{ state.vault.name }}</h1>
      </div>
      <div class="col-12">
        <h5> Keeps: {{ state.keeps.length }}</h5>
      </div>
      <div class="card-columns">
        <VaultKeepComponent v-for="keep in state.keeps" :key="keep.id" :keeps-props="keep" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { logger } from '../utils/Logger'
import { vaultKeepService } from '../services/VaultKeepService'
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
import { vaultService } from '../services/VaultService'
import { accountService } from '../services/AccountService'
export default {
  name: 'VaultDetailsPage',
  props: {},
  setup() {
    const route = useRoute()
    const state = reactive({
      keeps: computed(() => AppState.activeVaultKeeps),
      vault: computed(() => AppState.activeVault),
      account: computed(() => AppState.account),
      newVaultKeep: {

      }
    })
    onMounted(async() => {
      try {
        await vaultService.getVaultById(route.params.id)
      } catch (error) {
        logger.log(error)
      }
      try {
        await vaultService.getKeepsByVaultId(route.params.id, state.account)
      } catch (error) {
        logger.log(error)
      }
      try {
        await accountService.getAccount()
      } catch (error) {
        logger.log(error)
      }
    })
    return {
      state,

      async removeKeepFromVault() {
        try {
          const choice = confirm('Are you sure you want to remove this Keep from the vault?')
          if (choice === true) {
            vaultKeepService.removeFromVault()
          } else {
            alert('The keep has not been removed from the vault')
          }
        } catch (error) {
          logger.log(error)
        }
      }

    }
  }
}
</script>

<style lang="scss" scoped>

</style>
