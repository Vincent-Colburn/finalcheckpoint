<template>
  <div class="container-fluid">
    <div class="row py-5">
      <div class="col-5  text-right">
        <img class="img-fluid profilePicture" :src="state.profile.picture" alt="">
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
        <h1>
          Vaults: <i class="fa fa-plus text-success" data-toggle="modal" v-if="state.account.id == state.profile.id" data-target="#newVaultModal" aria-hidden="true"><h1>
          </h1>

          </i>
        </h1>
        <div class="modal fade"
             id="newVaultModal"
             tabindex="-1"
             role="dialog"
             aria-labelledby="modelTitleId"
             aria-hidden="true"
        >
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title">
                  Create New Vault
                </h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                <div class="row">
                  <div class="col">
                    <form @submit.prevent="createVault()">
                      <div class="form-group very-light-blue">
                        <label for="exampleInputEmail1">Title</label>
                        <input type="string" placeholder="Title..." class="form-control" v-model="newVault.name">
                      </div>
                      <div class="form-group very-light-blue">
                        <label for="exampleInputEmail1">Description</label>
                        <textarea type="string"
                                  style="mind-width: 100%"
                                  placeholder="Description..."
                                  class="form-control"
                                  rows="3"
                                  v-model="newVault.description"
                        >
                        </textarea>
                      </div>
                      <div class="form-group">
                        <input type="checkbox" class=" mx-2" v-model="newVault.isPrivate">
                        <label for="checkbox" class="col-xs-2 control-label">Vault Private?</label>
                        <div class="col-xs-10 mx-2 soSmall">
                          <small class="text-muted"> Private vaults can only be seen by you </small>
                        </div>
                        <div>
                        </div>
                      </div>
                    </form>
                  </div>
                </div>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">
                  Close
                </button>
                <button type="submit" @click="createVault()" class="btn btn-primary">
                  Add Vault
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="container-fluid">
      <div class="row d-flex mx-0 my-1 vaults">
        <VaultComponent v-for="vault in state.vaults" :key="vault.id" :vaults-props="vault" />
      </div>
    </div>

    <div class="row">
      <div class="col-12">
        <h1>Keeps:  <i class="fa fa-plus text-success" v-if="state.account.id == state.profile.id" data-toggle="modal" data-target="#newKeepModal" aria-hidden="true"></i> </h1>
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
import { onMounted, reactive, computed } from 'vue'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { profileService } from '../services/ProfileService'
import { useRoute } from 'vue-router'
import { keepService } from '../services/KeepService'
import $ from 'jquery'
import { vaultService } from '../services/VaultService'
export default {
  name: 'ProfileDetailsPage',
  setup() {
    const route = useRoute()
    const state = reactive({
      profile: computed(() => AppState.activeProfile),
      vaults: computed(() => AppState.profileVaults),
      account: computed(() => AppState.account),
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
      state,
      newKeep: {
        creatorId: state.account.id
      },
      newVault: {
        creatorId: state.account.id
      },
      createKeep() {
        // console.log('should be your new keep', this.newKeep)
        keepService.createKeep(this.newKeep)
        this.newKeep = {}
        $('#newKeepModal').modal('toggle')
      },
      createVault() {
        console.log('this should be your new vault', this.newVault)
        vaultService.createVault(this.newVault)
        this.newVault = {}
        $('#newVaultModal').modal('toggle')
      }
    }
  }
}
</script>

<style lang="scss" scoped>
@import "bootstrap";
.card-columns {
  @include media-breakpoint-only(xl) {
    column-count: 3;
  }
  @include media-breakpoint-only(lg) {
    column-count: 3;
  }
  @include media-breakpoint-only(md) {
    column-count: 3;
  }
  @include media-breakpoint-only(sm) {
    column-count: 2;
  }
}
.vaults{
  display: inline-block;

}
.soSmall{
  margin-top: 0px;
  padding: 0;
}
.profilePicture{
  min-height: 300px;
  min-width: auto;
}
</style>
