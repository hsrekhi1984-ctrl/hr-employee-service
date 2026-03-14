{{- define "microservice-base.name" -}}
{{- default .Values.service.name .Values.nameOverride | trunc 63 | trimSuffix "-" -}}
{{- end -}}

{{- define "microservice-base.fullname" -}}
{{- if .Values.fullnameOverride -}}
{{- .Values.fullnameOverride | trunc 63 | trimSuffix "-" -}}
{{- else -}}
{{- $name := include "microservice-base.name" . -}}
{{- if contains $name .Release.Name -}}
{{- .Release.Name | trunc 63 | trimSuffix "-" -}}
{{- else -}}
{{- printf "%s-%s" .Release.Name $name | trunc 63 | trimSuffix "-" -}}
{{- end -}}
{{- end -}}
{{- end -}}

{{- define "microservice-base.chart" -}}
{{- printf "%s-%s" .Chart.Name .Chart.Version | replace "+" "_" | trunc 63 | trimSuffix "-" -}}
{{- end -}}

{{- define "microservice-base.labels" -}}
helm.sh/chart: {{ include "microservice-base.chart" . }}
{{ include "microservice-base.selectorLabels" . }}
app.kubernetes.io/version: {{ .Values.image.tag | quote }}
app.kubernetes.io/managed-by: {{ .Release.Service }}
{{- end -}}

{{- define "microservice-base.selectorLabels" -}}
app.kubernetes.io/name: {{ include "microservice-base.name" . }}
app.kubernetes.io/instance: {{ .Release.Name }}
{{- end -}}

{{- define "microservice-base.serviceAccountName" -}}
{{- if .Values.serviceAccount.create -}}
{{- default (include "microservice-base.fullname" .) .Values.serviceAccount.name -}}
{{- else -}}
{{- default "default" .Values.serviceAccount.name -}}
{{- end -}}
{{- end -}}
