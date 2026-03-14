# hr-employee-service values

This directory now contains service-specific Helm values consumed by the shared
`helm/microservice-base` chart.

Install example:

```bash
helm upgrade --install hr-employee-service ./helm/microservice-base \
  -n hr --create-namespace -f ./helm/hr-employee-service/values.yaml
```
