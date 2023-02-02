# Future Improvements

## Business
1. Handle uniqueness of employee records (could be base on firstname, lastname, gender and birthdate).
2. Add filter and or keyword search for a better user experience.
3. Add pagination to ensure that it can support big number of employees to elimiate future performance issue.
4. Store the basic monthly salary and daily rate to the database.
5. Replace the Javascript Alert and Confirm call with the UI framework modal to make the system more presentable

## Technical
1. Introduce Object Mapper like AutoMapper to reduce code duplication.
2. Improve Configuring Entities by files instead of putting everything in ApplicationDbContext.OnModelCreating, when the number of table keeps on growing.
3. Application Insight Integration for a better logging and monitoring.
4. Implement fully automated CICD with the following scope:
	- Dev notification on PR creation
	- Automated code coverage validation
	- Automated static code analysis
	- Automated owasp dependency validation
	- Automated API testing
	- Build and releases (pre approval and notification if needed)

## Nice-To-Have
1. Improve API documentation by introducing Swagger (OpenAPI Spec 3.0)