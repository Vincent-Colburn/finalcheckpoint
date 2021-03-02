<template>
  <div class="container-fluid">
    <div class="row py-5">
      <div class="col-3 offset-2">
        <img :src="state.profile.picture" alt="">
      </div>
      <div class="col-7">
        <h1>
          {{ state.profile.name }}
        </h1>
        <h4>
          Vaults: {{ state.vaults.length }}
        </h4>
        <h4> Keeps: {{ state.keeps.length }}        </h4>
      </div>
    </div>
    <div class="row">
      <div class="col-12">
      </div>
    </div>
  </div>
</template>

<script>
import { onMounted, reactive, computed } from 'vue'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { profileService } from '../services/ProfileService'
import { useRoute } from 'vue-router'
export default {
  name: 'ProfileDetailsPage',

  setup() {
    const route = useRoute()
    const state = reactive({
      profile: computed(() => AppState.activeProfile),
      vaults: computed(() => AppState.profileVaults),
      keeps: computed(() => AppState.profileKeeps)
    })
    onMounted(async() => {
      try {
        await profileService.getProfileById(route.params.id)
      } catch (error) {
        logger.log(error)
      }
      try {
        await profileService.getVaultsByProfileId(route.params.id)
      } catch (error) {
        logger.log(error)
      }
      try {
        await profileService.getKeepsByProfileId(route.params.id)
      } catch (error) {
        logger.log(error)
      }
    })
    return {
      state
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
