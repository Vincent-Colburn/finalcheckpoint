// import { AppState } from '../AppState'
import { api } from './AxiosService'
import { vaultService } from './VaultService'

class VaultKeepService {
  async createVaultKeep(newVaultKeep) {
    const res = await api.post('api/vaultkeeps', newVaultKeep)
    console.log('this is your new vaultkeep', res)
    vaultService.getVaultById(newVaultKeep.vaultId)
  }
}

export const vaultKeepService = new VaultKeepService()
