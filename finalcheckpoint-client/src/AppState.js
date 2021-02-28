import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  account: {},
  accountVaults: [],
  profileVaults: [],
  profileKeeps: [],
  activeProfile: {},
  keeps: [],
  activeKeep: {},
  activeKeeps: [],
  vaults: [],
  activeVault: {},
  vaultKeeps: []

})
