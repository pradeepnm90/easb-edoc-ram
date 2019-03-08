
export class Subdivisions {
  id?: number;
  name: string;
  sortOrder: number;
  isSelected: boolean;
  subDivisions?: Subdivisions[];

  constructor(target?: {
    id?: number;
    name: string;
    sortOrder: number;
    isSelected: boolean;
    subdivisions?: Subdivisions[];
  }) {
    this.id = target.id;
    this.name = target.name;
    this.sortOrder = target.sortOrder;
    this.isSelected = target.isSelected;
    this.subDivisions = target.subdivisions;
  }
}
