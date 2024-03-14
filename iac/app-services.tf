resource "azurerm_service_plan" "dekra" {
  name                = "dekra-free-plan"
  resource_group_name = azurerm_resource_group.dekra.name
  location            = azurerm_resource_group.dekra.location
  sku_name            = "F1"
  os_type             = "Linux"
}

resource "azurerm_linux_web_app" "dekra" {
  name                = "james-todo"
  resource_group_name = azurerm_resource_group.dekra.name
  location            = azurerm_service_plan.dekra.location
  service_plan_id     = azurerm_service_plan.dekra.id

  site_config {
    always_on         = false
    application_stack {
      dotnet_version = var.dotnet_version
    }
  }
}