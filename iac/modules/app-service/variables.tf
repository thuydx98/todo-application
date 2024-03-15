variable "resource_group_name" {
  type      = string
  nullable  = false
}

variable "location" {
  type      = string
  nullable  = false
}

variable "service_plan_name" {
  type      = string
  nullable  = false
}

variable "service_plan_sku_name" {
  type      = string
  nullable  = false
}

variable "service_plan_os_type" {
  type      = string
  nullable  = false
}

variable "web_app_name" {
  type      = string
  nullable  = false
}

variable "web_app_always_on" {
  type      = bool
  nullable  = false
  default   = false
}

variable "dotnet_version" {
  type      = string
  nullable  = true
}

variable "keyvault_name" {
  description = "Name of using key vault"
  type        = string
  nullable    = false
}