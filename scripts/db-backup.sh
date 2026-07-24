#!/bin/bash
# Dev-DB backup (brief §5 M0). Dumps in custom format (-Fc) so pg_restore can do
# parallel, selective restores later - not just a flat SQL replay.
#
# Usage: scripts/db-backup.sh [output-dir]
# Env overrides: PGHOST, PGPORT, PGUSER, PGPASSWORD, PGDATABASE
set -euo pipefail

OUT_DIR="${1:-./backups}"
PGHOST="${PGHOST:-localhost}"
PGPORT="${PGPORT:-5432}"
PGUSER="${PGUSER:-devuser}"
PGDATABASE="${PGDATABASE:-greenfolio}"
export PGPASSWORD="${PGPASSWORD:-devpassword}"

mkdir -p "$OUT_DIR"
TIMESTAMP=$(date -u +%Y%m%dT%H%M%SZ)
OUT_FILE="$OUT_DIR/${PGDATABASE}_${TIMESTAMP}.dump"

echo "Backing up $PGDATABASE@$PGHOST:$PGPORT -> $OUT_FILE"
pg_dump -h "$PGHOST" -p "$PGPORT" -U "$PGUSER" -d "$PGDATABASE" -Fc -f "$OUT_FILE"

echo "Done: $(du -h "$OUT_FILE" | cut -f1) $OUT_FILE"
