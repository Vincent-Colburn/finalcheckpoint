<template>
  <div class="container-fluid">
    <!-- <div class="row test"> -->
    <div class="masonry">
      <KeepComponent v-for="keep in state.keeps" :key="keep.id" :keeps-props="keep" />
    </div>
    <!-- </div> -->
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
// import { useRoute, useRouter } from 'vue-router'
import { AppState } from '../AppState'
import { keepService } from '../services/KeepService'
import { logger } from '../utils/Logger'
// import { profileService } from '../services/ProfileService'
export default {
  name: 'Home',
  props: {
    keepsProps: { type: Object, required: true }
  },
  setup(props) {
    // const router = useRouter()
    const state = reactive({
      keeps: computed(() => AppState.keeps),
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.accountVaults)
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
.masonry { /* Masonry container */
  column-count: 5;
  column-gap: 1em;
}

.item { /* Masonry bricks or child elements */
  background-color: #eee;
  display: inline-block;
  margin: 0 0 1em;
  width: 100%;
}
// @import "bootstrap";

// @media (min-width: 992px) {
//  .card-columns {column-count: 3;}
// }
// @media (min-width: 1200px) {
//    .card-columns {column-count: 3;
//    }
// }

// .card-columns {
//   @include media-breakpoint-only(xl) {
//     padding: 20px;
//     column-count: 3;
//   }
  // @include media-breakpoint-down(xl) {
  //   display: block;
  // }
  // @include media-breakpoint-only(lg) {
  //   column-count: 3;
  // }
  // @include media-breakpoint-only(md) {
  //   column-count: 3;
  // }
  // @include media-breakpoint-only(sm) {
  //   column-count: 2;
  // }
// }

// .card-columns {
//   column-count: 4;
// }
// .card-columns .card{
//   display: block;
// }
// .home{
//   text-align: center;
//   user-select: none;
//   > img{
//     height: 200px;
//     width: 200px;
//   }
// }

// .home {
//   font-family: sans-serif;
//    margin: 0;
//    background: #f2f2f2;
// }

// h1 {
//   text-align: center;
//   margin-top: 50px;
// }

// p {
//   text-align: center;
//   margin-bottom:60px;
// }

// h4{
//   text-align:center;
//   line-height:80px;
//   font-weight:normal;

// }

// .masonry { /* Masonry container */
//     -webkit-column-count: 4;
//   -moz-column-count:4;
//   column-count: 4;
//   -webkit-column-gap: 1em;
//   -moz-column-gap: 1em;
//   column-gap: 1em;
//    margin: 1.5em;
//     padding: 0;
//     -moz-column-gap: 1.5em;
//     -webkit-column-gap: 1.5em;
//     column-gap: 1.5em;
//     font-size: .85em;
// }
// .item {
//     display: inline-block;
//     background: #fff;
//     padding: 1em;
//     margin: 0 0 1.5em;
//     width: 100%;
//     -webkit-transition:1s ease all;
//     box-sizing: border-box;
//     -moz-box-sizing: border-box;
//     -webkit-box-sizing: border-box;
//     box-shadow: 2px 2px 4px 0 #ccc;
// }
// .item img{max-width:100%; height: auto;}

// @media only screen and (max-width: 320px) {
//     .masonry {
//         -moz-column-count: 1;
//         -webkit-column-count: 1;
//         column-count: 1;
//     }
// }

// @media only screen and (min-width: 321px) and (max-width: 768px){
//     .masonry {
//         -moz-column-count: 2;
//         -webkit-column-count: 2;
//         column-count: 2;
//     }
// }
// @media only screen and (min-width: 769px) and (max-width: 1200px){
//     .masonry {
//         -moz-column-count: 3;
//         -webkit-column-count: 3;
//         column-count: 3;
//     }
// }
// @media only screen and (min-width: 1201px) {
//     .masonry {
//         -moz-column-count: 4;
//         -webkit-column-count: 4;
//         column-count: 4;
//     }
// }
</style>
