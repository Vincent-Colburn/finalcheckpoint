<template>
  <!-- <div class="about text-center">
    <h1>Welcome {{ account.name }}</h1>
    <img class="rounded" :src="account.picture" alt="" />
    <p>{{ account.email }}</p>
  </div> -->
  <div class="container-fluid">
    <div class="row py-5">
      <div class="col-3 py-3 text-right">
        <img class="img-fluid" :src="state.account.picture" alt="">
      </div>
      <div class="col-7">
        <h1>
          {{ state.account.name }}
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
    <div class="row">
      <div class="col-12">
        <h1>Keeps:  <i class="fa fa-plus text-success" data-toggle="modal" data-target="#newKeepModal" aria-hidden="true"></i> </h1>
        <!-- Button trigger modal -->
        <!-- Modal -->
        <div class="modal fade"
             id="newKeepModal"
             tabindex="-1"
             role="dialog"
             aria-labelledby="modelTitleId"
             aria-hidden="true"
        >
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title">
                  Create New Keep
                </h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                <div class="row">
                  <div class="col">
                    <form @submit.prevent="createKeep()">
                      <div class="form-group very-light-blue">
                        <label for="exampleInputEmail1">Title</label>
                        <input type="string" placeholder="Title..." class="form-control" v-model="newKeep.name">
                      </div>
                      <div class="form-group very-light-blue">
                        <label for="exampleInputEmail1">img</label>
                        <input type="string" placeholder="URL..." class="form-control" v-model="newKeep.img">
                      </div>
                      <div class="form-group very-light-blue">
                        <label for="exampleInputEmail1">Description</label>
                        <textarea type="string"
                                  style="mind-width: 100%"
                                  placeholder="Description..."
                                  class="form-control"
                                  rows="3"
                                  v-model="newKeep.description"
                        >
                        </textarea>
                      </div>
                    </form>
                  </div>
                </div>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">
                  Close
                </button>
                <button type="submit" @click="createKeep()" class="btn btn-primary">
                  Add Keep
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="card-columns">
        <ProfileKeepComponent v-for="keep in state.keeps" :key="keep.id" :keeps-props="keep" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
export default {
  name: 'Account',
  setup() {
    const state = reactive({
      profile: computed(() => AppState.activeProfile),
      vaults: computed(() => AppState.profileVaults),
      account: computed(() => AppState.Account),
      keeps: computed(() => AppState.profileKeeps)
    })
    return {
      state,
      account: computed(() => AppState.account)
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
