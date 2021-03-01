<template>
  <div class="component">
  </div>
</template>

<script>
import { reactive } from 'vue'
import { logger } from '../utils/Logger'
import { vaultService } from '../services/VaultService'
export default {
  // remember to look for this if it's not populating
  name: 'VaultModal',
  setup() {
    const state = reactive({
      newVault: {}
    })
    return {
      state,
      async createKeep() {
        try {
          await vaultService.createVault(state.newVault)
        } catch (error) {
          logger.log(error)
        }
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
