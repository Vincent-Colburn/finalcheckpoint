import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('api/account')
      AppState.account = res.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  // TODO this may not work, I think that you might have to pass the account id
  async getVaults() {
    const data = this.getAccount()
    const id = data.id
    const res = await api.get('api/account/' + id + '/vaults')
    AppState.accountVaults = res.data
  }
}

export const accountService = new AccountService()
