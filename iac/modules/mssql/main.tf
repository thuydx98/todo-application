resource "azurerm_mssql_server" "mssql_server" {
  name                         = var.sqlserver_name
  resource_group_name          = var.resource_group_name
  location                     = var.location
  version                      = var.sqlserver_version
  administrator_login          = var.database_admin
  administrator_login_password = var.database_admin_password
  minimum_tls_version          = "1.2"
}

resource "azurerm_mssql_firewall_rule" "mssql_firewall_rule" {
  name             = var.mssql_firewall_rule_name
  server_id        = azurerm_mssql_server.mssql_server.id
  start_ip_address = var.firewall_rule_start_ip_address
  end_ip_address   = var.firewall_rule_end_ip_address
}

resource "azurerm_mssql_firewall_rule" "allow_azure_resources" {
  name             = "allow_azure_resources"
  server_id        = azurerm_mssql_server.mssql_server.id
  start_ip_address = "0.0.0.0"
  end_ip_address   = "0.0.0.0"
}

resource "azurerm_mssql_database" "mssql" {
  server_id            = azurerm_mssql_server.mssql_server.id
  name                 = var.database_name
  collation            = var.database_collation
  sku_name             = var.database_sku
  max_size_gb          = 2
  storage_account_type = "Local"
}
