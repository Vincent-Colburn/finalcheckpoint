<template>
  <div class="home flex-grow-1 d-flex flex-column align-items-center justify-content-center">
    <img src="https://bcw.blob.core.windows.net/public/img/8600856373152463" alt="CodeWorks Logo">
    <h1 class="my-5 bg-dark text-light p-3 rounded d-flex align-items-center">
      <span class="mx-2 text-white">Vue 3 Starter</span>
    </h1>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
// import { useRoute, useRouter } from 'vue-router'
import { AppState } from '../AppState'
import { keepService } from '../services/KeepService'
import { logger } from '../utils/Logger'
export default {
  name: 'Home',
  setup() {
    // const router = useRouter()
    const state = reactive({
      keeps: computed(() => AppState.keeps)
    })

    onMounted(async() => {
      try {
        await keepService.getKeeps()
      } catch (error) {
        logger.error(error)
      }
    })
    return { state }
  }
}
</script>

<style scoped lang="scss">
.home{
  text-align: center;
  user-select: none;
  > img{
    height: 200px;
    width: 200px;
  }
}
</style>
