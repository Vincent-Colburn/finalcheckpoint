import { AppState } from '../AppState'
import { api } from './AxiosService'
import { profileService } from './ProfileService'

class VaultService {
  async getVaultById(id) {
    const res = await api.get('api/vaults/' + id)
    AppState.activeVault = res.data
  }

  async createVault(newVault) {
    const res = await api.post('api/vaults', newVault)
    this.getVaultById(res.id)
  }

  async deleteVault(vault) {
    await api.delete('api/vaults/' + vault.id)
    profileService.getVaultsByProfileId(vault.creatorId)
  }

  async getKeepsByVaultId(id) {
    const res = await api.get('api/vaults/' + id + '/keeps')
    AppState.activeVaultKeeps = res.data
  }
}
export const vaultService = new VaultService()
