resource "random_password" "mssql_root_password" {
  length           = 16
  special          = true
  lower            = true
  upper            = true
  numeric          = true
  override_special = "!$#%"
}

module "resource_group" {
  source              = "./modules/resource-group"
  resource_group_name = var.resource_group_name
  location            = var.location
}

module "app_service" {
  source                = "./modules/app-service"
  depends_on            = [module.resource_group]
  resource_group_name   = var.resource_group_name
  location              = var.location
  service_plan_name     = var.service_plan_name
  service_plan_sku_name = var.service_plan_sku_name
  service_plan_os_type  = var.service_plan_os_type
  web_app_name          = var.web_app_name
  dotnet_version        = var.dotnet_version
  keyvault_name         = var.keyvault_name
}

module "mssql" {
  source                  = "./modules/mssql"
  depends_on              = [module.resource_group]
  resource_group_name     = var.resource_group_name
  location                = var.location
  sqlserver_name          = var.sqlserver_name
  database_admin          = "dekra"
  database_admin_password = random_password.mssql_root_password.result

  mssql_firewall_rule_name       = var.mssql_firewall_rule_name
  firewall_rule_start_ip_address = var.mssql_firewall_allow_ip
  firewall_rule_end_ip_address   = var.mssql_firewall_allow_ip

  database_name      = "todo"
  database_collation = "SQL_Latin1_General_CP1_CI_AS"
  database_sku       = var.database_sku
}

module "keyvault" {
  source                  = "./modules/key-vault"
  depends_on              = [module.resource_group, module.app_service]
  resource_group_name     = var.resource_group_name
  location                = var.location
  keyvault_name           = var.keyvault_name
  todo_app_object_id      = module.app_service.web_app_principal_id
  sqlserver_name          = var.sqlserver_name
  sqlserver_root_password = random_password.mssql_root_password.result
}
