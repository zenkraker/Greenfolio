#!/bin/bash
# Dev-DB restore (brief §5 M0). Restores a -Fc dump produced by db-backup.sh into a
# target database, dropping and recreating it first so the restore is a true replacement,
# not a merge into whatever was already there.
#
# Usage: scripts/db-restore.sh <dump-file> [target-database]
# Env overrides: PGHOST, PGPORT, PGUSER, PGPASSWORD
set -euo pipefail

DUMP_FILE="${1:?Usage: db-restore.sh <dump-file> [target-database]}"
TARGET_DB="${2:-greenfolio}"
PGHOST="${PGHOST:-localhost}"
PGPORT="${PGPORT:-5432}"
PGUSER="${PGUSER:-devuser}"
export PGPASSWORD="${PGPASSWORD:-devpassword}"

if [ ! -f "$DUMP_FILE" ]; then
  echo "Dump file not found: $DUMP_FILE" >&2
  exit 1
fi

echo "Restoring $DUMP_FILE -> $TARGET_DB@$PGHOST:$PGPORT"

psql -h "$PGHOST" -p "$PGPORT" -U "$PGUSER" -d postgres -v ON_ERROR_STOP=1 \
  -c "DROP DATABASE IF EXISTS \"$TARGET_DB\";" \
  -c "CREATE DATABASE \"$TARGET_DB\" OWNER \"$PGUSER\";"

pg_restore -h "$PGHOST" -p "$PGPORT" -U "$PGUSER" -d "$TARGET_DB" --no-owner --role="$PGUSER" "$DUMP_FILE"

echo "Restore complete."
