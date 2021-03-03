<template>
  <a class="dropdown-item"
     href="#"
     @click.prevent="createVaultKeep(vaultsProps.id, keepsProps.id)"
  >
    {{
      vaultsProps.name }}
  </a>
</template>

<script>
import { logger } from '../utils/Logger'
import { vaultKeepService } from '../services/VaultKeepService'
import $ from 'jquery'
export default {
  name: 'DropdownComponent',
  props: {
    vaultsProps: { type: Object, required: true },
    keepsProps: { type: Object, required: true }
  },
  setup(props) {
    return {
      createVaultKeep(vaultId, keepId) {
        try {
          vaultKeepService.createVaultKeep(vaultId, keepId)
          $('#keepModal').modal('toggle')
        } catch (error) {
          logger.log(error)
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
