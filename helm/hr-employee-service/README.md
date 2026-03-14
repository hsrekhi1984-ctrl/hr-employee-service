# hr-employee-service Helm Chart

This chart deploys the HR Employee Service API with enterprise defaults:

- Pod security hardening (`runAsNonRoot`, dropped Linux capabilities, read-only root FS)
- Health probes and rolling updates
- HPA and PodDisruptionBudget
- Optional Ingress, NetworkPolicy, and ServiceMonitor
- Environment-specific values files for staging and production

## Install

```bash
helm upgrade --install hr-employee-service ./helm/hr-employee-service \
  --namespace hr \
  --create-namespace \
  -f ./helm/hr-employee-service/values-staging.yaml
```

## Required values

You should set at least:

- `image.repository`
- `image.tag`
- `secretEnv.ConnectionStrings__PostgreSql`

## Validate

```bash
helm lint ./helm/hr-employee-service
helm template hr-employee-service ./helm/hr-employee-service -f ./helm/hr-employee-service/values-staging.yaml >/dev/null
```
