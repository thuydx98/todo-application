terraform {
  required_version = ">= 1.1.0"
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.95.0"
    }
  }
  cloud {
    organization = "dekra"
    workspaces {
      name = "dekra-todo"
    }
  }
}

provider "azurerm" {
  features {}
}
