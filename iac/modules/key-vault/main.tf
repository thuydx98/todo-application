data "azurerm_client_config" "current" {}

resource "azurerm_key_vault" "key_vault" {
  name                          = var.keyvault_name
  location                      = var.location
  resource_group_name           = var.resource_group_name
  tenant_id                     = data.azurerm_client_config.current.tenant_id
  sku_name                      = "standard"
  enabled_for_disk_encryption   = true
  public_network_access_enabled = true
  soft_delete_retention_days    = 7
}

resource "azurerm_key_vault_access_policy" "app_registrstion_access_policy" {
  key_vault_id       = azurerm_key_vault.key_vault.id
  tenant_id          = data.azurerm_client_config.current.tenant_id
  object_id          = data.azurerm_client_config.current.object_id
  secret_permissions = ["Get", "List", "Set", "Delete", "Recover"]
}

resource "azurerm_key_vault_access_policy" "todo_app_access_policy" {
  key_vault_id       = azurerm_key_vault.key_vault.id
  tenant_id          = data.azurerm_client_config.current.tenant_id
  object_id          = var.todo_app_object_id
  secret_permissions = ["Get", "List"]
}

resource "azurerm_key_vault_secret" "key_vault_sqlserver_root_password" {
  name         = "mssql-root-password"
  value        = var.sqlserver_root_password
  key_vault_id = azurerm_key_vault.key_vault.id
  depends_on   = [azurerm_key_vault_access_policy.app_registrstion_access_policy]
}

resource "azurerm_key_vault_secret" "database_connection_string" {
  name         = "ConnectionStrings--Database"
  value        = "Server=${var.sqlserver_name}.database.windows.net;Database=todo;User Id=dekra;Password=${var.sqlserver_root_password};"
  key_vault_id = azurerm_key_vault.key_vault.id
  depends_on   = [azurerm_key_vault_access_policy.app_registrstion_access_policy]
}
