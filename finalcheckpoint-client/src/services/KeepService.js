import { AppState } from '../AppState'
import { api } from './AxiosService'
import { profileService } from './ProfileService'

class KeepService {
  async getKeeps() {
    const res = await api.get('/api/keeps')
    AppState.keeps = res.data
    // console.log('these should be your keeps', AppState.keeps)
  }

  async getKeepById(id) {
    const res = await api.get('api/keeps/' + id)
    AppState.activeKeep = res.data
  }

  async createKeep(keepData) {
    await api.post('api/keeps', keepData)
    this.getKeeps()
  }

  async editKeep(keep) {
    await api.post('api/keeps/' + keep.id, keep)
    this.getKeeps(keep.Id)
  }

  async deleteKeep(keep) {
    await api.delete('api/keeps/' + keep.id)
    profileService.getKeepsByProfileId(keep.creatorId)
  }
}

export const keepService = new KeepService()
