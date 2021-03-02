import { AppState } from '../AppState'
import { api } from './AxiosService'

class ProfileService {
  async getProfileById(id) {
    const res = await api.get('api/profiles/' + id)
    AppState.activeProfile = res.data
  }

  async getKeepsByProfileId(id) {
    const res = await api.get('api/profiles/' + id + '/keeps')
    AppState.profileKeeps = res.data
    console.log('keeps for profile', res.data)
  }

  async getVaultsByProfileId(id) {
    const res = await api.get('api/profiles/' + id + '/vaults')
    AppState.profileVaults = res.data
    // console.log('vaults for profile', res.data)
  }
}

export const profileService = new ProfileService()
