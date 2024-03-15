resource "azurerm_service_plan" "service_plan" {
  name                = var.service_plan_name
  resource_group_name = var.resource_group_name
  location            = var.location
  sku_name            = var.service_plan_sku_name
  os_type             = var.service_plan_os_type
}

resource "azurerm_linux_web_app" "web_app" {
  name                = var.web_app_name
  resource_group_name = var.resource_group_name
  location            = var.location
  service_plan_id     = azurerm_service_plan.service_plan.id

  identity { type = "SystemAssigned" }

  site_config {
    always_on = var.web_app_always_on
    application_stack {
      dotnet_version = var.dotnet_version
    }
  }

  app_settings = {
    ASPNETCORE_ENVIRONMENT = "Production"
    KeyVaultName           = var.keyvault_name
  }
}
