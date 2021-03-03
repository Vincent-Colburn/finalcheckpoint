// import { AppState } from '../AppState'

import { api } from './AxiosService'
import { vaultService } from './VaultService'

class VaultKeepService {
  async createVaultKeep(vaultId, keepId) {
    const newVaultKeep = { vaultId, keepId }
    const res = await api.post('api/vaultkeeps', newVaultKeep)
    console.log('this is your new vaultkeep', res)
    vaultService.getVaultById(newVaultKeep.vaultId)
  }

  async removeFromVault(keep) {
    await api.delete('api/vaultkeeps/' + keep.vaultKeepId)
    vaultService.getVaultById(keep.vaultId)
  }
}

export const vaultKeepService = new VaultKeepService()
