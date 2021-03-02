<template>
  <div class="col-2">
    <router-link :to="{name: 'VaultDetailsPage', params: {id: vaultsProps.id}}">
      <div class="card vaultCard rounded">
        <div class="card-top text-right">
          <i class="fa fa-trash text-danger" v-if="state.account.id == state.profile.id" @click="deleteVault()" aria-hidden="true"></i>
        </div>
        <div class="card-body">
          <h4 class="card-title vaultName fixed-bottom mx-2">
            {{ vaultsProps.name }}
          </h4>
        </div>
      </div>
    </router-link>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
import { vaultService } from '../services/VaultService'
// import { keepService } from '../services/KeepService'
export default {
  name: 'VaultComponent',
  props: {
    vaultsProps: { type: Object, required: true }
  },
  setup(props) {
    const state = reactive({
      vaults: computed(() => AppState.vaults),
      account: computed(() => AppState.account),
      profile: computed(() => AppState.activeProfile)
    })
    return {
      state,

      async deleteVault(vaultsProp) {
        const choice = confirm('Are you sure you want to delete this keep? It is irreversible')
        if (choice === true) {
          vaultService.deleteVault(props.vaultsProps)
        } else {
          alert('Keep was not deleted')
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.vaultCard{
  background-color: #b2bec3;
  min-height: 200px;
  min-width: auto;
  max-width: 200px;
}
.vaultName{
  position: absolute;
}
// .card{
//   display: flex;
// }
</style>
