import { AppState } from '../AppState'
import { api } from './AxiosService'
import { profileService } from './ProfileService'

class VaultService {
  async getVaultById(id) {
    const res = await api.get('api/vaults/' + id)
    AppState.activeVault = res.data
    console.log('over here', res.data)
  }

  async createVault(newVault) {
    const res = await api.post('api/vaults', newVault)
    this.getVaultById(res.id)
  }

  async deleteVault(vault) {
    await api.delete('api/vaults/' + vault.id)
    profileService.getVaultsByProfileId(vault.creatorId)
  }

  async getKeepsByVaultId(id, account) {
    // const vault = this.getVaultById(id)
    // if (vault.IsPrivate === true) {
    //   if (vault.creatorId === account.id) {
    //     const res = await api.get('api/vaults/' + id + '/keeps')
    //     AppState.activeVaultKeeps = res.data
    //   } else {
    //     console.log('you cannot view private vaults')
    //   }
    // } else {
    const res = await api.get('api/vaults/' + id + '/keeps', account)
    AppState.activeVaultKeeps = res.data
    // }
  }
}
export const vaultService = new VaultService()
