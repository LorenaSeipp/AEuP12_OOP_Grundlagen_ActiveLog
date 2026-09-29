#!/usr/bin/env bash
# Ermittelt, welche Story(s) ein Pull Request betrifft.
# Wird von ci.yml und mebis-abgabe-beleg.yml genutzt.
#
# Ausgabe: zweistellige Story-Nummern, durch Leerzeichen getrennt (z. B. "06 07"),
#          oder nichts, wenn keine Story erkennbar ist.
#
# Reihenfolge der Quellen (die erste mit Treffer gewinnt):
#   1. PR-Titel        "Story 03: Intensität abgesichert"  -> 03
#   2. Branch-Name     "story03"                           -> 03
#   3. alle Commits zwischen BASE_SHA und HEAD_SHA, z. B. bei einem PR über
#      zwei Stories: "Story06: ...", "Story 07: ..."        -> 06 07
#
# Eingaben (Umgebungsvariablen, alle optional): PR_TITLE, HEAD_REF, BASE_SHA, HEAD_SHA

# Erkennt u. a. "Story 04", "Story4", "story_4", "Story-04", "Story #4", "Story: 4",
# "Story Nr. 4", "Stories 4", "UserStory4" (Groß-/Kleinschreibung egal).
# Nicht: "Story 123", "history 5".
nummern() {
  grep -ioE '(^|[^a-z0-9]|user)stor(y|ie|ies)[ _.:#-]*(nr[ _.:#-]*)?[0-9]{1,2}([^0-9]|$)' | grep -oE '[0-9]+' | while read -r n; do
    printf '%02d\n' "$((10#$n))"
  done
}

for quelle in "${PR_TITLE:-}" "${HEAD_REF:-}"; do
  treffer=$(printf '%s' "$quelle" | nummern | head -n1)
  if [ -n "$treffer" ]; then
    echo "$treffer"
    exit 0
  fi
done

if [ -n "${BASE_SHA:-}" ] && [ -n "${HEAD_SHA:-}" ] && git cat-file -e "${BASE_SHA}^{commit}" 2>/dev/null; then
  git log --format='%s%n%b' "$BASE_SHA..$HEAD_SHA" | nummern | sort -u | tr '\n' ' ' | sed 's/ $//'
fi
