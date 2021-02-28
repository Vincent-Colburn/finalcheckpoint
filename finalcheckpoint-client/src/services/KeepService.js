import { AppState } from '../AppState'
import { api } from './AxiosService'

class KeepService {
  async getKeeps() {
    const res = await api.get('/api/keeps')
    AppState.Keeps = res.data
    console.log('these should be your keeps', res.data)
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
    await api.post('api/keeps/' + keep.Id, keep)
    this.getKeeps(keep.Id)
  }

  async deleteKeep(id) {
    await api.delete('api/vaults/' + id)
  }
}

export const keepService = new KeepService()
