# Database-Challenges

This repository contains sql-challenges to stay fit with sql-stuff :)

# Setup

Database: https://www.imdb.com/interfaces/

**cmd:**
```
docker exec -it postgres psql -U gast -d <database>
```

```sql
DROP TABLE IF EXISTS Actor;
CREATE TABLE Actor(nconst text PRIMARY KEY, primaryName text, birthyear integer, deathyear integer, primaryprofession text[], knownfortitles text[]);
COPY actor (nconst, primaryname, birthyear, deathyear, primaryprofession, knownfortitles) FROM '/var/lib/postgresql/data/actors.tsv' DELIMITER E'\t' NULL as '\N' CSV HEADER;

DROP TABLE IF EXISTS Basic;
CREATE TABLE Basic(tconst text PRIMARY KEY, titleType text, primaryTitle text, originalTitle text, isAdult integer, startYear integer, endYear integer, runtimeMinutes integer, genres text[]);
COPY basic (tconst, titletype, primarytitle, originaltitle, isadult, startyear, endyear, runtimeminutes, genres) FROM '/var/lib/postgresql/data/basics.tsv' DELIMITER E'\t' NULL as '\N' CSV HEADER;

DROP TABLE IF EXISTS Crew;
CREATE TABLE Crew(tconst text PRIMARY KEY, directors text[], writers text[]);
COPY crew (tconst, directors, writers) FROM '/var/lib/postgresql/data/crew.tsv' DELIMITER E'\t' NULL as '\N' CSV HEADER;

DROP TABLE IF EXISTS Episode;
CREATE TABLE Episode(tconst text PRIMARY KEY, parentTconst text, seasonNumber integer, episodeNumber integer);
COPY episode (tconst, parentTconst, seasonNumber, episodeNumber) FROM '/var/lib/postgresql/data/episode.tsv' DELIMITER E'\t' NULL as '\N' CSV HEADER;

DROP TABLE IF EXISTS Principal;
CREATE TABLE Principal(tconst text, ordering integer, nconst text, category text, job text, characters text);
COPY principal (tconst, ordering, nconst, category, job, characters) FROM '/var/lib/postgresql/data/principals.tsv' DELIMITER E'\t' NULL as '\N' CSV HEADER;

DROP TABLE IF EXISTS Rating;
CREATE TABLE Rating(tconst text PRIMARY KEY, averagerating real, numvotes integer);
COPY rating (tconst, averagerating, numvotes) FROM '/var/lib/postgresql/data/ratings.tsv' DELIMITER E'\t' NULL as '\N' CSV HEADER;

DROP TABLE IF EXISTS Title;
CREATE TABLE Title(tconst text PRIMARY KEY, ordering integer, title text, region text, language text, types text, attributes text, isOriginalTitle integer);
COPY title (tconst, ordering, title, region, language, types, attributes, isOriginalTitle) FROM '/var/lib/postgresql/data/episode.tsv' DELIMITER E'\t' NULL as '\N' CSV HEADER;
```
## Errors:

NOTICE:  table "basic" does not exist, skipping
DROP TABLE
CREATE TABLE
ERROR:  invalid input syntax for type integer: "{Animation,Comedy}"
CONTEXT:  COPY basic, line 678502, column runtimeminutes: "{Animation,Comedy}"

NOTICE:  table "principal" does not exist, skipping
DROP TABLE
CREATE TABLE
ERROR:  missing data for column "characters"
CONTEXT:  COPY principal, line 387626: "tt0042387       7       nm0788565       writer  based on a story "Paradise Lost '49' by \N
tt0042387       8       nm0770127       compos..."

NOTICE:  table "title" does not exist, skipping
DROP TABLE
CREATE TABLE
ERROR:  invalid input syntax for type integer: "tt15180956"
CONTEXT:  COPY title, line 2, column ordering: "tt15180956"

## Challenge 1: (22.02.2022 - 13.03.2022)

Get the name of the actors with the most films before the year 2000 (endYear <= 2000)
