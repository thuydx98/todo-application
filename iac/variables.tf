variable "environment" {
  description = "Development environment"
  type        = string
  nullable    = false
  default = "dev"
}

variable "resource_group_name" {
  description = "Name of the Resource Group"
  type        = string
  nullable    = false
  default = "dekra"
}

variable "location" {
  description = "Location of resources"
  type        = string
  nullable    = false
  default = "eastasia"
}

variable "service_plan_name" {
  description = "Name of service plan"
  type        = string
  nullable    = false
  default = "free-service-plan"
}

variable "service_plan_sku_name" {
  description = "SKU name of service plan"
  type        = string
  nullable    = false
  default = "B1"
}

variable "service_plan_os_type" {
  description = "OS of service plan"
  type        = string
  nullable    = false
  default = "Linux"
}

variable "web_app_name" {
  description = "App service name"
  type        = string
  nullable    = false
  default = "james-todo"
}

variable "dotnet_version" {
  description = ".NET version of app service"
  type        = string
  nullable    = true
  default = "8.0"
}

variable "sqlserver_name" {
  description = "SQL server name"
  type        = string
  nullable    = false
  default = "dekra-db-server"
}

variable "mssql_firewall_rule_name" {
  description = "Name of MSSQL firewall rule"
  type        = string
  nullable    = false
  default = "developers"
}

variable "mssql_firewall_allow_ip" {
  description = "Allow IP for MSSQL firewall"
  type        = string
  nullable    = false
  default = "14.173.195.113"
}

variable "database_sku" {
  description = "Database SKU"
  type        = string
  nullable    = false
  default = "Basic"
}

variable "keyvault_name" {
  description = "Name of key vault"
  type        = string
  nullable    = false
  default = "dekra-todo"
}