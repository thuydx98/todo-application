variable "resource_group_name" {
  type = string
  nullable  = false
}

variable "location" {
  type = string
  nullable  = false
}

variable "sqlserver_version" {
  type = string
  nullable  = false
  default = "12.0"
}

variable "database_name" {
  type = string
  nullable  = false
}

variable "database_admin" {
  type = string
  nullable  = false
}

variable "database_admin_password" {
  type      = string
  sensitive = true
  nullable  = false
}

variable "mssql_firewall_rule_name" {
  type = string
  nullable  = false
}

variable "firewall_rule_start_ip_address" {
  type = string
  nullable  = false
}

variable "firewall_rule_end_ip_address" {
  type = string
  nullable  = false
}

variable "sqlserver_name" {
  type = string
  nullable  = false
}

variable "database_collation" {
  type = string
  nullable  = false
}

variable "database_sku" {
  type = string
  nullable  = false
}
