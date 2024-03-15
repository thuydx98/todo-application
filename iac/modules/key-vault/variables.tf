variable "keyvault_name" {
  type = string
  nullable = false
}

variable "location" {
  type = string
  nullable = false
}

variable "resource_group_name" {
  type = string
  nullable = false
}

variable "todo_app_object_id" {
  type = string
  nullable = false
}

variable "sqlserver_name" {
  type = string
  nullable = false
}

variable "sqlserver_root_password" {
  type = string
  sensitive = true
  nullable = false
}
                                                                                                                                                                                                            
