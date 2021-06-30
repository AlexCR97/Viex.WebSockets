import storage from "@/storage";

export default {
    int(min: number, max: number) {
        min = Math.ceil(min);
        max = Math.floor(max);
        return Math.floor(Math.random() * (max - min + 1)) + min;
    },

    gameConcepts() {
        const concepts = storage.local.getGameConceptCategories();
        const uniqueConcepts = new Set<string>();

        while (uniqueConcepts.size < 5) {
            const index = this.int(0, concepts.length - 1);
            const randomConcept = concepts[index];
            uniqueConcepts.add(randomConcept);
        }

        return [ ...uniqueConcepts ];
    }
}
