<template>
  <div class="component">
  </div>
</template>

<script>
// import { computed, onMounted, reactive } from 'vue'
// import { logger } from '../utils/Logger'
// import { vaultKeepService } from '../services/VaultKeepService'
// import { AppState } from '../AppState'
// import { useRouter, useRoute } from 'vue-router'
// import { vaultService } from '../services/VaultService'
// export default {
//   name: 'VaultDetailsPage',
//   setup() {
//     const router = useRoute()
//     const state = reactive({
//       keeps: computed(() => AppState.activeVaultKeeps),
//       newVaultKeep: {

//       }
//     })
//     onMounted(async() => {
//       try {
//         await vaultService.getVaultById(route.params.id)
//       } catch (error) {
//         logger.log(error)
//       }
//       try {
//         await vaultService.getKeepsByVaultId(route.params.id)
//       } catch (error) {
//         logger.log(error)
//       }
//     })
//     return {
//       state,

//       async removeKeepFromVault() {
//         try {
//           const choice = confirm('Are you sure you want to remove this Keep from the vault?')
//           if (choice === true) {
//             vaultKeepService.createVaultKeep(state.newVaultKeep)
//             state.newVaultKeep = {}
//           } else {
//             alert('The keep has not been removed from the vault')
//           }
//         } catch (error) {
//           logger.log(error)
//         }
//       }

//     }
//   }
// }
</script>

<style lang="scss" scoped>

</style>
