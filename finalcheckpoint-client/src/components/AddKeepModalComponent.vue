<template>
  <div class="component">
  </div>
</template>

<script>
import { reactive } from 'vue'
import { keepService } from '../services/KeepService'
import { logger } from '../utils/Logger'
export default {
  // remember to look for this if it's not populating
  name: 'KeepModal',
  setup() {
    const state = reactive({
      newKeep: {}
    })
    return {
      state,
      async createKeep() {
        try {
          await keepService.createKeep(state.newKeep)
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
