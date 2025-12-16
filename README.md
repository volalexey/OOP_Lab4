# Lab 4: Static Members and String Conversion

## Description
This update introduces static class members to manage global state and implements standard string conversion logic for the `Planet` class.

## Objectives
* Implement static fields and properties.
* Create static factory methods (`Parse`, `TryParse`).
* Override `ToString` for object serialization.

## Key Features
* **Global State:** Usage of static fields to track metrics, such as the total count of created objects.
* **Data Conversion:**
    * `ToString()` override for standardized string output.
    * `Parse` and `TryParse` static methods to reconstruct objects from string inputs.
* **Menu Update:** Added functionality to demonstrate static method execution.
