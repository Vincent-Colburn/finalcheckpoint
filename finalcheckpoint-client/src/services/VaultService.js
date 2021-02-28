import { AppState } from '../AppState'
import { api } from './AxiosService'

class VaultService {
  async getVaultById(id) {
    const res = await api.get('api/vaults/' + id)
    AppState.activeVault = res.data
  }

  async createVault(newVault) {
    const res = await api.post('api/vaults', newVault)
    this.getVaultById(res.id)
  }

  async deleteVault(id) {
    await api.delete('api/vaults/' + id)
  }

  async getKeepsByVaultId(id) {
    const res = await api.get('api/vaults/' + id + '/keeps')
    AppState.vaultKeeps = res.data
  }
}
export const vaultService = new VaultService()
