resource "azurerm_mssql_server" "dekra" {
  name                         = "james-to-mssql"
  resource_group_name          = var.resource_group_name
  location                     = var.location
  version                      = "12.0"
  administrator_login          = var.database_admin
  administrator_login_password = var.database_admin_password
  minimum_tls_version          = "1.2"
}