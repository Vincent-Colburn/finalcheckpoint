<template>
  <!-- Modal -->
  <div class="KeepModalComponent"
       :id="props.keepsProps.id"
       tabindex="-1"
       role="dialog"
       aria-labelledby="modelTitleId"
       aria-hidden="true"
  >
    <div class="modal-dialog" role="document">
      <div class="modal-content">
        <button type="button" class="close text-right" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
        <!-- <div class="row">
          <div class="col-6 imgModal pt-0 pb-4 ml-3 py-0">
            <img class="rounded" :src="props.keepsProps.img" alt="" style="width:100%">
          </div>
          <div class="col-5 mr-5">
            <div class="row">
              <div class="col-1 offset-10 eye text-right d-flex">
                <i class="fa fa-eye text-success mx-1 py-1 text-right" aria-hidden="true"></i><p>
                  {{ props.keepsProps.views }}
                </p>
              </div>
              <div class="col-1 thumbtack d-flex text-left justify-content-left align-items-left">
                <i class="fa fa-thumb-tack text-success py-1 mx-1" aria-hidden="true"></i>
                <p> {{ props.keepsProps.keeps }} </p>
              </div>
            </div>
            <div class="col-12 py-5 text-center">
              <h1> {{ props.keepsProps.name }}</h1>
            </div>
            <div class="col-8 offset-2 text-weight-light">
              {{ props.keepsProps.description }}
            </div>
            <div class="col-12 border-bottom bold py-3">
            </div>
            <div class="col-12">
            </div>
            <div class="row bottomKeepModal py-5">
              <div class="col-4 py-5 text-center">
                <div class="dropdown text-success border">
                  <button class="btn toggler dropdown-toggle button text-success"
                          type="button"
                          id="dropdownMenuButton"
                          data-toggle="dropdown"
                  >
                    ADD TO VAULT:
                  </button>
                  <div class="dropdown-menu" aria-labelledby="drowpdownMenuButton">
                    <DropdownComponent v-for="vault in state.vaults" :key="vault.id" :vaults-props="vault" :keeps-props="keepsProps" />
                  </div>
                </div>
              </div>
              <div class="col-2 py-5 text-center">
                <i class="fa fa-trash fa-10x " v-if="state.account.id == props.keepsProps.creatorId" @click="deleteKeep()" aria-hidden="true"></i>
              </div>
              <div class="col-5 py-5 text-right d-flex">
                <router-link :to="{ name: 'ProfileDetailsPage', params: { id: props.keepsProps.creatorId}}">
                  <img class="card-img-profile profile rounded img-fluid" :src="props.keepsProps.creator.picture" alt="">
                </router-link>
                <h5 class="">
                  {{ props.keepsProps.creator.name }}
                </h5>
              </div>
            </div>
          </div>
        </div> -->
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'KeepModalComponent',
  props: {
    keepsProps: { type: Object, required: true }
  },
  setup() {
    return {}
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
