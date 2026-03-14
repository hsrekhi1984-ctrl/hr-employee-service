# HR Helm Charts

This repository uses a reusable base chart at `helm/microservice-base`.
Each service supplies only values overrides.

## Install commands

```bash
helm upgrade --install hr-employee-service ./helm/microservice-base \
  -n hr --create-namespace -f ./helm/hr-employee-service/values.yaml

helm upgrade --install hr-payroll-service ./helm/microservice-base \
  -n hr --create-namespace -f ./helm/hr-payroll-service/values.yaml

helm upgrade --install hr-notification-service ./helm/microservice-base \
  -n hr --create-namespace -f ./helm/hr-notification-service/values.yaml

helm upgrade --install hr-leave-service ./helm/microservice-base \
  -n hr --create-namespace -f ./helm/hr-leave-service/values.yaml

helm upgrade --install hr-frontend ./helm/microservice-base \
  -n hr --create-namespace -f ./helm/hr-frontend/values.yaml
```
